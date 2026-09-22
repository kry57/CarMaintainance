using AutoMapper;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Cars;
using CarMaintenance.Application.Services.Products;
using CarMaintenance.Infrastructre.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class ProductRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService) : IProductRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
    }
}
