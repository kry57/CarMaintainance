using AutoMapper;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Cars;
using CarMaintenance.Infrastructre.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class CarRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService) : ICarsRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
    }
}
