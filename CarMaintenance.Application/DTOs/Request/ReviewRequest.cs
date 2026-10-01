using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Application.DTOs.Request
{
    public class ReviewRequest
    {
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(500)]
        public string Comment { get; set; } = string.Empty;
    }
}
