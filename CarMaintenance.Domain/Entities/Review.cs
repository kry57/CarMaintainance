using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Domain.Entities
{
    public class Review : AuditableEntity
    {
        public string CustomerId { get; set; } = string.Empty;
        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = default!;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty; 
    }
}
