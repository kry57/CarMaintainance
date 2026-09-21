using CarMaintenance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Application.Services.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericReposatory<Provider> Providers { get; }
        IGenericReposatory<Product> Products { get; }
        IGenericReposatory<Car> Cars { get; }
        IGenericReposatory<Subscription> Subscriptions { get; }
        IGenericReposatory<RegistrationRequest> RegistrationRequests { get; }
        IGenericReposatory<Review> Reviews { get; }
        IGenericReposatory<ProviderCategoryType> ProviderCategoryTypes { get; }
        
    }

}
