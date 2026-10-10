using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Application.DTOs.Request
{
    public class UpdateMyProfileRequest
    {
        [Required]
        public string FullName   { get; set; } = string.Empty;
    }
}
