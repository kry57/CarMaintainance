using AutoMapper;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Cars;
using CarMaintenance.Application.Services.Interfaces;
using CarMaintenance.Application.Services.Products;
using CarMaintenance.Application.Services.ProviderCategoriesTypes;
using CarMaintenance.Application.Services.Providers;
using CarMaintenance.Application.Services.RegistrationsRequests;
using CarMaintenance.Application.Services.Reviews;
using CarMaintenance.Application.Services.Subscriptions;
using CarMaintenance.Infrastructre.Context;
using CarMaintenance.Infrastructre.Implementations;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IProviderRepository Providers { get; }
        public IProductRepository Products { get; }
        public ICarsRepository Cars { get; }
        public ISubscriptionsRepository Subscriptions { get; }
        public IRegistrationRequestsRepository RegistrationRequests { get; }
        public IReviewRepository Reviews { get; }
        public IProviderCategoryTypesRepository ProviderCategoryTypes { get; }

        public UnitOfWork(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, IImageService imageService)
        {
            _context = context;

            Providers = new ProviderRepository(context, mapper, currentUserService);
            Products = new ProductRepository(context, mapper, currentUserService, imageService);
            Cars = new CarRepository(context, mapper, currentUserService);
            Subscriptions = new SubscriptionRepository(context, mapper, currentUserService);
            RegistrationRequests = new RegistrationsRequestsRepository(context, mapper, currentUserService);
            Reviews = new ReviewRepository(context, mapper, currentUserService);
            ProviderCategoryTypes = new ProviderCategoryRepository(context, mapper, currentUserService);
        }
    }
}