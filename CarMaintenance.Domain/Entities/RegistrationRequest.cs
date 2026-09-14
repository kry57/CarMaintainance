using CarMaintenance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Domain.Entities
{
    public class RegistrationRequest : AuditableEntity
    {
        
        public string CustomerId { get; set; } = string.Empty;
        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = default!;
        public RequestStatus RequestStatus { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
