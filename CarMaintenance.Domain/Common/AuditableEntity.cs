using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Domain.Common
{
    public abstract  class AuditableEntity : BaseEntity
    {
        public DateTime  CreatedAt { get; set; } = DateTime.UtcNow;
        public string?  CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
