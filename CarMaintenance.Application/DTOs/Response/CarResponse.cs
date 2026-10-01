using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.DTOs.Response
{
    public class CarResponse
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public bool IsDelete { get; set; } = false;
    }
}
