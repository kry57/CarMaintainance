using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarMaintainance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegistrationsRequestController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpPost("create/{providerId}")]
        public async Task<IActionResult> Add([FromRoute] int providerId, [FromBody] RegistrationRequestDto request)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _unitOfWork.RegistrationRequests.AddAsync(customerId, providerId, request);

            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { Id = result.ValueOuter!.Id }, result.ValueOuter) : result.ToProblem(400);

        }
        [HttpGet("get-by-Id/{Id}")]
        public IActionResult GetById([FromRoute] int Id )
        {
            return Ok();
        }
        
        [HttpPut("respond/{customerId}/{providerId:int}")]
        public async Task<IActionResult> RespondToRequest([FromRoute] string customerId, [FromRoute] int providerId)
        {
            var responderId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _unitOfWork.RegistrationRequests.GetStatusAsync(customerId, providerId);

            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(statusCode: 400);
        }
        
        [HttpPut("cancel/{requestId:int}")]
        public async Task<IActionResult> Cancel([FromRoute] int requestId)
        {
            var callerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _unitOfWork.RegistrationRequests.CancelAsync(callerId, requestId);

            return result.IsSuccess ? NoContent() : result.ToProblem(statusCode: 400);
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetMyRequestsAsync()
        {
            var callerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _unitOfWork.RegistrationRequests.GetMyRequestsAsync(callerId);

            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(statusCode: 400);
        }
        [HttpGet("provider/{providerId:int}")]
        public async Task<IActionResult> GetProviderRequestsAsync([FromRoute] int providerId, [FromQuery] RequestStatus? status)
        {
            var callerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _unitOfWork.RegistrationRequests.GetProviderRequestsAsync(callerId,providerId,status);

            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(statusCode: 400);
        }

        
        [HttpGet("provider/{providerId:int}/booked-slots")]
        public async Task<IActionResult> GetBookedSlots([FromRoute] int providerId, [FromQuery] DateOnly date)
        {
            var result = await _unitOfWork.RegistrationRequests.GetBookedSlotsAsync(providerId, date);

            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(statusCode: 400);
        }
    }
}
