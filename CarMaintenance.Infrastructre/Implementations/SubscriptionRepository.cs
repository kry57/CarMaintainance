using AutoMapper;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.RegistrationsRequests;
using CarMaintenance.Application.Services.Subscriptions;
using CarMaintenance.Infrastructre.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class SubscriptionRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService) : ISubscriptionsRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
    }
}
