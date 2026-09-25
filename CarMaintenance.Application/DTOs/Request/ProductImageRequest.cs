using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Application.DTOs.Request
{
    public class ProductImageRequest
    {
        [Required]
        public IFormFile Image { get; set; } = default!;

        public bool IsPrimary { get; set; } = false;
    }
}