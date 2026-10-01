using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.Services
{
    public interface ISubscriptionExpiryJob
    {
        Task ExpireSubscriptionsAsync();
    }
}
