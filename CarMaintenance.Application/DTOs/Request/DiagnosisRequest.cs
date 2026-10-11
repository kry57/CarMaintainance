using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Application.DTOs.Request
{

    public class DiagnosisRequest
    {
        [Required]
        [MinLength(3)]
        public string Problem { get; set; } = string.Empty;
    }
}
