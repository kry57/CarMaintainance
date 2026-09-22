using CarMaintenance.Application.Services.Cars;
using CarMaintenance.Application.Services.Products;
using CarMaintenance.Application.Services.ProviderCategoriesTypes;
using CarMaintenance.Application.Services.Providers;
using CarMaintenance.Application.Services.RegistrationsRequests;
using CarMaintenance.Application.Services.Reviews;
using CarMaintenance.Application.Services.Subscriptions;
using CarMaintenance.Domain.Entities;


namespace CarMaintenance.Application.Services.Interfaces
{
    public interface IUnitOfWork
    {
        IProviderRepository Providers { get; }
        IProductRepository Products { get; }
        ICarsRepository Cars { get; }
        ISubscriptionsRepository Subscriptions { get; }
        IRegistrationRequestsRepository RegistrationRequests { get; }
        IReviewRepository Reviews { get; }
        IProviderCategoryTypesRepository ProviderCategoryTypes { get; }
        
    }

}
