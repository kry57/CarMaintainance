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

        //public async Task<Result<ProviderResponse>> AddAsync(ProviderRequest request)
        //{
        //    if (request is null)
        //        return Result<ProviderResponse>.Failure(ProviderError.InvalidData);

        //    var provider = _mapper.Map<Provider>(request);
        //    provider.OwnerId = _currentUser.UserId;   

        //    var result = await _unitOfWork.Providers.AddAsync(provider);
        //    var response = _mapper.Map<ProviderResponse>(provider);
        //    return result ? Result<ProviderResponse>.Success(response) : Result<ProviderResponse>.Failure(ProviderError.InvalidData);
        //}

       
    }
}
