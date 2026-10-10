using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;


namespace CarMaintenance.Application.Services.AccountService
{
    public interface IAccountService
    {
        // As a Normal User -_- 
        Task<AccountResponse> Me();
        Task<Result> UpdateMyProfile(UpdateMyProfileRequest request);
        Task<Result> ChangePassword(ChangePasswordRequest request);
    }
}
