using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.ProviderErrorProvider;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Providers;
using CarMaintenance.Infrastructre.Context;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
namespace CarMaintenance.Infrastructre.Implementations
{
    public class ProviderRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        : IProviderRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<ProviderResponse>> AddAsync(ProviderRequest request)
        {
            if (request is null)
                return Result<ProviderResponse>.Failure(ProviderError.InvalidData);

            var isDuplicate = await _context.Providers.AnyAsync(p =>p.Name == request.Name &&
            p.Address == request.Address && 
            p.Latitude == request.Latitude &&
            p.Longitude == request.Longitude && 
            p.PhoneNumber == request.PhoneNumber);

            if (isDuplicate)
                return Result<ProviderResponse>.Failure(ProviderError.DuplicatedProvider);

            var provider = _mapper.Map<Provider>(request);
            provider.OwnerId = _currentUserService.UserId;

            await _context.Providers.AddAsync(provider);
            var affectedRows = await _context.SaveChangesAsync();
            if (affectedRows <= 0)
                return Result<ProviderResponse>.Failure(ProviderError.InvalidData);

            var response = _mapper.Map<ProviderResponse>(provider);
            return Result<ProviderResponse>.Success(response);
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var provider = await _context.Providers.SingleOrDefaultAsync(p => p.Id == id);
            if (provider!.IsDeleted)
                return Result<ProviderResponse>.Failure(ProviderError.NotFound);
            var affectedRows = await _context.Providers
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();

            if (affectedRows <= 0)
                return Result.Failure(ProviderError.NotFound);

            return Result.Success();
        }

        public async Task<Result<IEnumerable<ProviderResponse>>> GetAllActivatedVerifiedAsync()
        {
            var providers = await _context.Providers
                .Where(p => p.IsActive && p.IsVerified && (p.IsDeleted == false))
                .ProjectTo<ProviderResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<IEnumerable<ProviderResponse>>.Success(providers);
        }

        public async  Task<Result<IEnumerable<ProviderResponse>>> GetAllAsync()
        {
            var providers = await _context.Providers
                  .ProjectTo<ProviderResponse>(_mapper.ConfigurationProvider)
                  .ToListAsync();
            return Result<IEnumerable<ProviderResponse>>.Success(providers);
        }

        public async Task<Result<ProviderResponse>> GetByIdAsync(int id)
        {
            var isUserIdExsited = await _context.Providers.FindAsync(id);
            if (isUserIdExsited is null)
                return Result<ProviderResponse>.Failure(ProviderError.NotFound);
            if (isUserIdExsited.IsDeleted)
                return Result<ProviderResponse>.Failure(ProviderError.NotFound);
            var response  = _mapper.Map<ProviderResponse>(isUserIdExsited);
            return Result<ProviderResponse>.Success(response);
        }

        public async Task<Result<ProviderResponse>> UpdateAsync(int id, ProviderRequest request)
        {
            var isDuplicate = await _context.Providers.AnyAsync(p => p.Id != id && p.Name == request.Name);
            if (isDuplicate)
                return Result<ProviderResponse>.Failure(ProviderError.DuplicatedProvider);

            var provider = await _context.Providers.SingleOrDefaultAsync(p => p.Id == id);

            if (provider is null)
                return Result<ProviderResponse>.Failure(ProviderError.NotFound);
            if(provider.IsDeleted)
                return Result<ProviderResponse>.Failure(ProviderError.NotFound);

            _mapper.Map(request, provider);

            var affectedRows = await _context.SaveChangesAsync();
            if (affectedRows <= 0)
                return Result<ProviderResponse>.Failure(ProviderError.InvalidData);

            var response = _mapper.Map<ProviderResponse>(provider);
            return Result<ProviderResponse>.Success(response);
        }
        public async Task<Result> ToggleStatus(int id, ProviderRequestStatus ProviderRequestStatus)
        {
            var provider = await _context.Providers.FindAsync(id);
            _mapper.Map(ProviderRequestStatus, provider);

            var affectedRows = await _context.SaveChangesAsync();
            if (affectedRows <= 0)
                return Result.Failure(ProviderError.InvalidData);
            return Result.Success();
        }
    }
}