using CarMaintenance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarMaintenance.Application.DTOs.Request
{
    public class ProductRequest
    {
        [Required]
        [MinLength(15)]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 1000000)]
        public decimal Price { get; set; }

        [Required]
        [EnumDataType(typeof(PartType))]
        public PartType PartType { get; set; }

        [Required]
        [Range(0, 1000000)]

        public int Quantity { get; set; } = 0;

        [Required]
        [MinLength(20)]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Brand { get; set; } = string.Empty;
    }
}
