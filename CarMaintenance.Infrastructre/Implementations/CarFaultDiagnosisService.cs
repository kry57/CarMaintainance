
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.DiagnosisErrorProvider;
using CarMaintenance.Application.Services;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarMaintenance.Infrastructre.Implementations;

public class CarFaultDiagnosisService : ICarFaultDiagnosisService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OllamaEmbeddingService _ollamaEmbeddingService;

    public CarFaultDiagnosisService(
        IHttpClientFactory httpClientFactory,
        OllamaEmbeddingService ollamaEmbeddingService)
    {
        _httpClientFactory = httpClientFactory;
        _ollamaEmbeddingService = ollamaEmbeddingService;
    }

    public async Task<Result<DiagnosisResponse>> DiagnoseAsync(
        DiagnosisRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Problem))
        {
            return Result<DiagnosisResponse>.Failure(
                DiagnosisError.EmptyProblem);
        }

        string filePath = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.UserProfile),
            "Downloads",
            "byd_f3_faults_en.jsonl");

        if (!File.Exists(filePath))
        {
            return Result<DiagnosisResponse>.Failure(
                DiagnosisError.DatasetNotFound);
        }

        string? matchedFault = null;
        double bestScore = double.NegativeInfinity;

        try
        {
            // 1. Generate the user's embedding only once.
            float[] problemEmbedding =
                await _ollamaEmbeddingService.GenerateEmbeddingAsync(
                    request.Problem.Trim(),
                    cancellationToken);

            // 2. Compare the user description with every fault record.
            foreach (string line in File.ReadLines(filePath))
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                using JsonDocument document = JsonDocument.Parse(line);

                JsonElement fault = document.RootElement;

                string title = fault.TryGetProperty(
                    "title", out JsonElement titleElement)
                    ? titleElement.GetString() ?? string.Empty
                    : string.Empty;

                string symptoms = fault.TryGetProperty(
                    "symptoms", out JsonElement symptomsElement)
                    ? symptomsElement.ValueKind == JsonValueKind.Array
                        ? string.Join(
                            " ",
                            symptomsElement.EnumerateArray()
                                .Select(x => x.GetString() ?? string.Empty))
                        : symptomsElement.GetString() ?? string.Empty
                    : string.Empty;

                string searchableText = $"{title}. {symptoms}";

                if (string.IsNullOrWhiteSpace(searchableText))
                    continue;

                float[] faultEmbedding =
                    await _ollamaEmbeddingService.GenerateEmbeddingAsync(
                        searchableText,
                        cancellationToken);

                double score = EmbeddingSimilarity.CosineSimilarity(
                    problemEmbedding,
                    faultEmbedding);

                if (score > bestScore)
                {
                    bestScore = score;
                    matchedFault = line;
                }
            }

            // 3. Reject matches below the initial threshold.
            // This threshold must be evaluated against real test examples.
            const double minimumScore = 0.30;

            if (matchedFault is null || bestScore < minimumScore)
            {
                return Result<DiagnosisResponse>.Failure(
                    DiagnosisError.FaultNotFound);
            }

            // 4. Ask Qwen to explain the selected record.
            string prompt = $"""
                You are a car fault information assistant.

                Rules:
                - Use only the selected JSON record below.
                - Do not claim you cannot access the dataset.
                - Do not invent causes, codes, or repair procedures.
                - The dataset is synthetic and unverified.
                - Clearly state that this is not a confirmed diagnosis.
                - If a field is missing, write "Not specified".

                Answer in this format:
                Fault:
                Possible causes:
                Diagnostic steps:
                Suggested fix:
                Severity:
                Driving advice:

                User's reported problem:
                {request.Problem}

                Selected fault record:
                {matchedFault}
                """;

            using HttpClient client =
                _httpClientFactory.CreateClient();

            using HttpResponseMessage response =
                await client.PostAsJsonAsync(
                    "http://localhost:11434/api/generate",
                    new
                    {
                        model = "qwen2.5:0.5b",
                        prompt,
                        stream = false
                    },
                    cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return Result<DiagnosisResponse>.Failure(
                    DiagnosisError.OllamaFailed);
            }

            using JsonDocument result = JsonDocument.Parse(
                await response.Content.ReadAsStringAsync(
                    cancellationToken));

            string answer = result.RootElement
                .GetProperty("response")
                .GetString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(answer))
            {
                return Result<DiagnosisResponse>.Failure(
                    DiagnosisError.EmptyAnswer);
            }

            return Result<DiagnosisResponse>.Success(
                new DiagnosisResponse
                {
                    Answer = answer,
                    MatchedFault = matchedFault,
                    MatchScore = bestScore,
                    IsVerified = false
                });
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (HttpRequestException)
        {
            return Result<DiagnosisResponse>.Failure(
                DiagnosisError.OllamaUnavailable);
        }
        catch (JsonException)
        {
            return Result<DiagnosisResponse>.Failure(
                DiagnosisError.InvalidDatasetOrResponse);
        }
        catch (IOException)
        {
            return Result<DiagnosisResponse>.Failure(
                DiagnosisError.DatasetReadFailed);
        }
    }
}
