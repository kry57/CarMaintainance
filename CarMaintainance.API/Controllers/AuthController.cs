using CarMaintainance.API.JWTProvider;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintainance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <param name="request">The registration details, including email, password, and required user info.</param>
        /// <returns>The created user's data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Auth/register
        ///     {
        ///        "email": "user@example.com",
        ///        "password": "P@ssw0rd!",
        ///        "confirmPassword": "P@ssw0rd!"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly registered user's data</response>
        /// <response code="400">If registration fails (e.g. validation errors, email already in use)</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterRequest),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return result.IsSuccess ? Ok() : result.ToProblem(400);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] SignInRequest request)
        {
            var result = await _authService.LogInAsync(request);
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpPost("generate-refresh-token")]
        public async Task<IActionResult> GenerateRefreshToken([FromBody] GenerateRefreshTokenRequest request)
        {
            var result = await _authService.GenerateRefreshToken(request.Token,request.RefreshToken);
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpPut("revoked-refresh-token")]
        public async Task<IActionResult> GetRevoked([FromBody] GenerateRefreshTokenRequest request)
        {
            var result = await _authService.GetRevoked(request.Token,request.RefreshToken);
            return result.IsSuccess ? NoContent() : result.ToProblem(400);
        }
        [HttpPut("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            var result = await _authService.ConfirmEmail(request.UserId,request.Code);
            return result.IsSuccess ?  NoContent(): result.ToProblem(400);
        }
        [HttpPut("resend-email-confirmation")]
        public async Task<IActionResult> ResendEmailConfirmation([FromBody] ResendEmailConfirmationRequest request)
        {
            var result = await _authService.ResendEmailConfirmation(request);
            return result.IsSuccess ?  NoContent(): result.ToProblem(400);
        }
        [HttpPut("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
        {
            var result = await _authService.ForgetPassword(request);
            return result.IsSuccess ?  NoContent(): result.ToProblem(400);
        }
        
        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _authService.ResetPassword(request);
            return result.IsSuccess ?  NoContent(): result.ToProblem(400);
        }
    }
}
