using CarMaintenance.Domain.Entities;
using CarMaintenance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Response
{
    public class ProductResonse
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string Brand { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; } 
        public PartType PartType { get; set; }
        public string Description { get; set; } = string.Empty;

    }
}
