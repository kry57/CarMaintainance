using AutoMapper;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.RegistrationsRequests;
using CarMaintenance.Application.Services.Reviews;
using CarMaintenance.Infrastructre.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class ReviewRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService) : IReviewRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
    }
}
