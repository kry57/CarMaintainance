using CarMaintenance.Application.Services.AccountService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintainance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var result  = await _accountService.Me();
            return Ok(result);
        }
        [HttpPut("update-my-info")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateMyProfileRequest request)
        {
            var result  = await _accountService.UpdateMyProfile(request);
            return result.IsSuccess ? NoContent() : result.ToProblem(400);
        }
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result  = await _accountService.ChangePassword(request);
            return result.IsSuccess ? NoContent() : result.ToProblem(400);
        }
    }
}
