using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;
using CarMaintenance.Application.ErrorProvider.UserErrorPrvider;
using CarMaintenance.Application.Services;
using CarMaintenance.Application.Services.AccountService;
using CarMaintenance.Infrastructre.Identity;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Identity;
using System.Net.WebSockets;


namespace CarMaintenance.Infrastructre.Implementations
{
    public class AccountService(ICurrentUserService currentUserService,UserManager<ApplicationUser> userManager,IMapper mapper)
        : IAccountService
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Result> ChangePassword(ChangePasswordRequest request)
        {
            var userId = _currentUserService.UserId;
            var user = await _userManager.FindByIdAsync(userId);
            var changePassword = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (changePassword.Succeeded)
                return Result.Success();
            var error = changePassword.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description));
        }

        public async Task<AccountResponse> Me()
        {
            var userId = _currentUserService.UserId;
            
            return await _userManager.Users
                .Where(u => u.Id == userId)
                .ProjectTo<AccountResponse>(_mapper.ConfigurationProvider)
                .SingleAsync();
        }

        public async Task<Result> UpdateMyProfile(UpdateMyProfileRequest request)
        {
            var userId = _currentUserService.UserId;
            var rows  =  await (_userManager.Users.ExecuteUpdateAsync(
                u => u.SetProperty(o => o.FullName , request.FullName)
                
                ));
            if (rows <= 0)
                return Result.Failure(UserError.NotUpdated);
            return Result.Success();
        }
    }
}
