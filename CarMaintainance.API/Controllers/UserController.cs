using CarMaintenance.Application.Services.AccountService;
using CarMaintenance.Application.Services.User;


namespace CarMaintainance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        
        private readonly IUserService _userService = userService;

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] UserRequest request)
        {
            var result  = await _userService.AddAsync(request);
            return result.IsSuccess ? CreatedAtAction(nameof(GetById),new { userId = result.ValueOuter!.Id } , result) : result.ToProblem(400); 
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] string userId)
        {
            var result = await _userService.GetByIdAsync(userId);
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromQuery] string userId , [FromBody] UpdateMyProfileRequest request)
        {
            var result = await _userService.UpdateAsync(userId, request);
            return result.IsSuccess ? NoContent() : result.ToProblem(400);
        }
    }
}
