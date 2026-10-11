using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Application.DTOs.Request
{
    public class UserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }     = string.Empty;
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FullName { get; set; }  = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

    }
}
