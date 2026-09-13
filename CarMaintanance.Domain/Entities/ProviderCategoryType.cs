using CarMaintanance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintanance.Domain.Entities
{
    public class ProviderCategoryType : BaseEntity
    {
        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = default!; 
        public ProviderCategory ProviderCategory { get; set; }
    }
}
