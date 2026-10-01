using CarMaintenance.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CarMaintainance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] SubscriptionRequest request)
        {
            var result  = await _unitOfWork.Subscriptions.AddAsync(request);
            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { SubscriptionId = result.ValueOuter!.Id }, result.ValueOuter) : result.ToProblem(400);
        }
        [HttpPut("cancel/{SubscriptionId}")]
        public async Task<IActionResult> Cancel([FromRoute] int SubscriptionId)
        {
            var result  = await _unitOfWork.Subscriptions.CancelAsync(SubscriptionId);
            return result.IsSuccess ? NoContent() : result.ToProblem(400);
        }

        [HttpPut("renew")]
        public async Task<IActionResult> Renew()
        {
            var result = await _unitOfWork.Subscriptions.RenewAsync();
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent()
        {
            var result = await _unitOfWork.Subscriptions.GetCurrentAsync();
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {;
            var result = await  _unitOfWork.Subscriptions.GetHistoryAsync();
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpPut("upgrade")]
        public async Task<IActionResult> Upgrade([FromBody] SubscriptionRequest request)
        {
            var result = await  _unitOfWork.Subscriptions.UpgradeAsync(request);
            return result.IsSuccess ? NoContent() : result.ToProblem(400);
        }
        [HttpPut("downgrade")]
        public async Task<IActionResult> Downgrade([FromBody] SubscriptionRequest request)
        {
            var result = await _unitOfWork.Subscriptions.DowngradeAsync(request);
            return result.IsSuccess ? NoContent() : result.ToProblem(400);
        }
        [HttpGet("get-by-id/{SubscriptionId}")]
        public async Task<IActionResult> GetById([FromRoute]  int SubscriptionId)
        {
            return Ok();
        }
    }
}
