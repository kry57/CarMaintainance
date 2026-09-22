using AutoMapper;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Cars;
using CarMaintenance.Application.Services.ProviderCategoriesTypes;
using CarMaintenance.Infrastructre.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class ProviderCategoryRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService) : IProviderCategoryTypesRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
    }
}
