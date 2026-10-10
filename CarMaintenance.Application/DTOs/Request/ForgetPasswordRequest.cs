using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Application.DTOs.Request
{
    public class ForgetPasswordRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } =string.Empty;
    }
}
