using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.DTOs.Response;


namespace CarMaintenance.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterRequest request);
        Task<Result<SignInResponse>> LogInAsync(SignInRequest request);
        Task<Result<SignInResponse>> GenerateRefreshToken(string token , string refreshToken);
        Task<Result> GetRevoked(string token , string refreshToken);
        Task<Result> ConfirmEmail(string UserId, string Code);
        Task<Result> ResendEmailConfirmation(ResendEmailConfirmationRequest request);
        Task<Result> ForgetPassword(ForgetPasswordRequest request);
        Task<Result> ResetPassword(ResetPasswordRequest request);

    }
}
