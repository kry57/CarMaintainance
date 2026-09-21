using CarMaintainance.API.JWTProvider;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintainance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] SignInRequest request)
        {
            var result = await _authService.LogInAsync(request);
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
    }
}
