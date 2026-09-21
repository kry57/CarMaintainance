namespace CarMaintenance.Domain.Entities
{
    public class Subscription : AuditableEntity
    {
        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = default!;
        public string PlanName { get; set; } = string.Empty;
        public DateTime StartAt { get; set; } = DateTime.UtcNow;
        public DateTime EndAt { get; set; } 
        public SubscriptionStatus SubscriptionStatus { get; set; }

    }
}
