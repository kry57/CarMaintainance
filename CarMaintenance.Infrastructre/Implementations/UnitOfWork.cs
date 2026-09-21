using CarMaintenance.Application.Services.Implementations;
using CarMaintenance.Application.Services.Interfaces;
using CarMaintenance.Infrastructre.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericReposatory<Provider> Providers {get;}

        public IGenericReposatory<Product> Products {get;}

        public IGenericReposatory<Car> Cars {get;}

        public IGenericReposatory<Subscription> Subscriptions {get;}

        public IGenericReposatory<RegistrationRequest> RegistrationRequests {get;}

        public IGenericReposatory<Review> Reviews {get;}

        public IGenericReposatory<ProviderCategoryType> ProviderCategoryTypes {get;}
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Providers = new GenericRepoastory<Provider>(context);
            Products = new GenericRepoastory<Product>(context);
            Cars = new GenericRepoastory<Car>(context);
            Subscriptions = new GenericRepoastory<Subscription>(context);
            RegistrationRequests = new GenericRepoastory<RegistrationRequest>(context);
            Reviews = new GenericRepoastory<Review>(context);
            ProviderCategoryTypes = new GenericRepoastory<ProviderCategoryType>(context);
        }

       
    }
}
