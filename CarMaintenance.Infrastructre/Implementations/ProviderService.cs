using AutoMapper;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.ProviderErrorProvider;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.Interfaces;
using CarMaintenance.Application.Services.Providers;

namespace CarMaintenance.Infrastructre.Implementations
{
    public class ProviderService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser) : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUser = currentUser;

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
    }
}
