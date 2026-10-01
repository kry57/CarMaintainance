using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Application.DTOs.Request
{
    public class SubscriptionRequest
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string PlanName { get; set; } = string.Empty;
    }
}