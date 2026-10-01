using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarMaintenance.Application.DTOs.Request
{
    public class CarRequest
    {
        [Required]
        [MaxLength(256)]
        [MinLength(3)]
        public string Make { get; set; } = string.Empty;
        [Required]
        [MaxLength(256)]
        [MinLength(2)]
        public string Model { get; set; } = string.Empty;
        [Required]
        [Range(1800 , 2026)]
        public int Year { get; set; }
        [Required]
        [MaxLength(256)]
        [MinLength(3)]
        public string PlateNumber { get; set; } = string.Empty;
    }
}
