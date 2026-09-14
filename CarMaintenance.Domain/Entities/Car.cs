using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CarMaintenance.Domain.Entities
{
    public class Car : AuditableEntity
    {
        public string CustomerId { get; set; } =string.Empty;
        public string Make {  get; set; }  = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
    }
}
