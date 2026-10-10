using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Application.DTOs.Request
{
    public class ResetPasswordRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Code { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;
    }
}
