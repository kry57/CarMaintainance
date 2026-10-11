using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class OllamaEmbeddingService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OllamaEmbeddingService(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<float[]> GenerateEmbeddingAsync(
            string text,
            CancellationToken cancellationToken = default)
        {
            using HttpClient client =
                _httpClientFactory.CreateClient();

            using HttpResponseMessage response =
                await client.PostAsJsonAsync(
                    "http://localhost:11434/api/embed",
                    new
                    {
                        model = "nomic-embed-text",
                        input = text
                    },
                    cancellationToken);

            response.EnsureSuccessStatusCode();

            using JsonDocument document = JsonDocument.Parse(
                await response.Content.ReadAsStringAsync(
                    cancellationToken));

            return document.RootElement
                .GetProperty("embeddings")[0]
                .EnumerateArray()
                .Select(value => value.GetSingle())
                .ToArray();
        }
    }
}
