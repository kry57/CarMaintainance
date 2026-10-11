using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintainance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DiagnosisController : ControllerBase
{
    private readonly ICarFaultDiagnosisService _diagnosisService;

    public DiagnosisController(
        ICarFaultDiagnosisService diagnosisService)
    {
        _diagnosisService = diagnosisService;
    }


    [HttpPost("chat")]
    public async Task<IActionResult> Diagnose(
        [FromBody] DiagnosisRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _diagnosisService.DiagnoseAsync(
            request,
            cancellationToken);

        if (result.IsSuccess)
            return Ok(result.ValueOuter);

        var statusCode = result.Error.Code switch
        {
            "Diagnosis.EmptyProblem" => 400,
            "Diagnosis.FaultNotFound" => 404,
            "Diagnosis.DatasetNotFound" => 500,
            "Diagnosis.DatasetReadFailed" => 500,
            "Diagnosis.OllamaUnavailable" => 503,
            "Diagnosis.OllamaFailed" => 502,
            "Diagnosis.EmptyAnswer" => 502,
            "Diagnosis.InvalidJson" => 502,
            _ => 500
        };

        return result.ToProblem(statusCode);
    }

}
