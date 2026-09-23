using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.ProviderErrorProvider;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Interfaces;
using CarMaintenance.Application.Services.Providers;
using CarMaintenance.Domain.Enums;
using CarMaintenance.Infrastructre.Context;
using CarMaintenance.Infrastructre.Identity;

using Microsoft.AspNetCore.Identity;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class ProviderService(IUnitOfWork unitOfWork, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper, ICurrentUserService currentUser) : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUser = currentUser;

        public async Task<Result<IEnumerable<ProviderResponse>>> GetProvidersByOwner(string id)
        {
            var owner = await _userManager.FindByIdAsync(id);
            if (owner == null)
                return Result<IEnumerable<ProviderResponse>>.Failure(ProviderError.NoProviderToThisOwner);

            var providersRelatedToThisOwner = await _context.Providers
                .Where(p => p.OwnerId == id)
                .ProjectTo<ProviderResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (!providersRelatedToThisOwner.Any())
                return Result<IEnumerable<ProviderResponse>>.Failure(ProviderError.NoProviderToThisOwner);

            return Result<IEnumerable<ProviderResponse>>.Success(providersRelatedToThisOwner);
        }

        public async Task<Result<IEnumerable<OwnerResponse>>> GetProvidersGroupedByOwner()
        {
            var group = from p in _context.Providers
                        group p by p.OwnerId into g
                        select new OwnerResponse
                        {
                            OwnerId = g.Key,
                            Providers = g.Select(p => _mapper.Map<ProviderResponse>(p)).ToList()
                        };
            var result = await group.ToListAsync();
            if (!result.Any())
                return Result<IEnumerable<OwnerResponse>>.Failure(ProviderError.NoProviderToThisOwner);

            return Result<IEnumerable<OwnerResponse>>.Success(result);

        }

        public async Task<Result> ToggleStatus(
     int id,
     bool? isActive,
     bool? isVerified,
     bool? isDeleted)
        {
            var result = await _unitOfWork.Providers.GetByIdAsync(id);

            if (result.IsFaliure)
                return Result.Failure(ProviderError.NotFound);

            var provider = _mapper.Map<Provider>(result.ValueOuter);

            if (isActive.HasValue)
                provider.IsActive = isActive.Value;

            if (isVerified.HasValue)
                provider.IsVerified = isVerified.Value;

            if (isDeleted.HasValue)
                provider.IsDeleted = isDeleted.Value;

            var request = _mapper.Map<ProviderRequestStatus>(provider);

            var updateResult = await _unitOfWork.Providers.ToggleStatus(id, request);

            if (updateResult.IsFaliure)
                return Result.Failure(updateResult.Error);

            return Result.Success();
        }
        // to be honest the calculation by ai -_*
        public async Task<Result<IEnumerable<ProviderResponse>>> GetNearbyProviders(double lat, double lng, double radiusKm, ProviderCategory? category = null)
        {
            var providers = await _context.Providers.Include(p => p.ProviderCategoryTypes)
                .Where(p => p.IsActive && p.IsVerified && (!p.IsDeleted))
                .ToListAsync();

            var nearby = providers
                .Select(p => new
                {
                    Provider = p,
                    Distance = CalculateDistanceKm(lat, lng, p.Latitude, p.Longitude)
                })
                .Where(x => x.Distance <= radiusKm)
                .Where(x => category == null || x.Provider.ProviderCategoryTypes.Any(pc => pc.ProviderCategory == category))
                .OrderBy(x => x.Distance)
                .Select(x => x.Provider)
                .ToList();

            if (!nearby.Any())
                return Result<IEnumerable<ProviderResponse>>.Failure(ProviderError.NotFoundByThisCategory);

            var response = _mapper.Map<List<ProviderResponse>>(nearby);
            return Result<IEnumerable<ProviderResponse>>.Success(response);
        }


        private static double CalculateDistanceKm(double lat1, double lng1, double lat2, double lng2)
        {
            const double R = 6371;

            double dLat = (lat2 - lat1) * Math.PI / 180;
            double dLng = (lng2 - lng1) * Math.PI / 180;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                       Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            return R * c;
        }
    }
}
