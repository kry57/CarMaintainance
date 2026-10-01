using CarMaintenance.Domain.Enums;

namespace CarMaintenance.Application.DTOs.Response
{
    public class SubscriptionResponse
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }

        public string PlanName { get; set; } = string.Empty;

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public SubscriptionStatus SubscriptionStatus { get; set; }
    }
}