using CarMaintanance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CarMaintanance.Domain.Entities
{
    public class Subscription : BaseEntity
    {
        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = default!;
        public string PlanName { get; set; } = string.Empty;
        public DateTime StartAt { get; set; } = DateTime.UtcNow;
        public DateTime EndAt { get; set; } = DateTime.UtcNow;
        public SubscriptionStatus SubscriptionStatus { get; set; }

    }
}
