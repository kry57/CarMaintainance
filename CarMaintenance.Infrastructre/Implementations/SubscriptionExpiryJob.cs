using CarMaintenance.Application.Services;
using CarMaintenance.Domain.Enums;
using CarMaintenance.Infrastructre.Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Infrastructre.Implementations
{
  
        public class SubscriptionExpiryJob(ApplicationDbContext context, ILogger<SubscriptionExpiryJob> logger) : ISubscriptionExpiryJob
        {
        public async Task ExpireSubscriptionsAsync()
        {
            var now = DateTime.UtcNow;

            var expired = await context.Subscriptions
                .Where(s => (s.SubscriptionStatus == SubscriptionStatus.Active || s.SubscriptionStatus == SubscriptionStatus.Upgraded)  && s.EndAt <= now)
                .ToListAsync();

            foreach (var sub in expired)
            {
                sub.SubscriptionStatus = SubscriptionStatus.Expired;

                if (sub.PendingPlanName is not null)
                {
                    context.Subscriptions.Add(new Subscription
                    {
                        ProviderId = sub.ProviderId,
                        PlanName = sub.PendingPlanName,
                        StartAt = now,
                        EndAt = now.AddMonths(1),
                        SubscriptionStatus = SubscriptionStatus.Active
                    });
                }
            }

            await context.SaveChangesAsync();

            if (expired.Count > 0)
                logger.LogInformation("Expired {Count} subscriptions", expired.Count);
        }
    }
    
}
