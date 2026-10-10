using CarMaintenance.Application.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Application.DTOs.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; } = string.Empty;
        [Required]
        [MustDifferentPassword(nameof(OldPassword))]
        public string NewPassword { get; set; } = string.Empty;
    }
}
