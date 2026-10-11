using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;


namespace CarMaintenance.Application.Services
{
    public interface ICarFaultDiagnosisService
    {
        Task<Result<DiagnosisResponse>> DiagnoseAsync(
         DiagnosisRequest request,
         CancellationToken cancellationToken = default);
    }
}
