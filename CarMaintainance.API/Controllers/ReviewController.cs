using CarMaintenance.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintainance.API.Controllers
{
    [Route("api/[controller]/{providerId}")]
    [ApiController]
   // [Authorize]
    public class ReviewController(IUnitOfWork unitOfWork): ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpPost("create")]
        public async Task<IActionResult> Add([FromRoute] int providerId , [FromBody] ReviewRequest request)
        {
            var result = await _unitOfWork.Reviews.AddAsync(providerId, request);
            return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { providerId, result.ValueOuter!.Id }, result.ValueOuter) : result.ToProblem(400);
        }
        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _unitOfWork.Reviews.GetByIdAsync(id);

            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);

        }
        [HttpDelete("delete/{reviewId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int providerId , [FromRoute] int reviewId)
        {
            var result = await _unitOfWork.Reviews.DeleteAsync(providerId, reviewId);

            return result.IsSuccess ? NoContent() : result.ToProblem(400);
        }
        [HttpPut("toggle-status/{reviewId:int}")]
        public async Task<IActionResult> toggle([FromRoute] int providerId , [FromRoute] int reviewId)
        {
            var result = await _unitOfWork.Reviews.ToggleStatusAsync(providerId, reviewId);

            return result.IsSuccess ? NoContent() : result.ToProblem(400);
        }
        [HttpPut("update/{reviewId:int}")]
        public async Task<IActionResult> toggle([FromRoute] int providerId , [FromRoute] int reviewId, [FromBody] ReviewRequest request)
        {
            var result = await _unitOfWork.Reviews.UpdateAsync(providerId, reviewId,request);

            return result.IsSuccess ? NoContent() : result.ToProblem(400);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] int? providerId)
        {
            var result = await _unitOfWork.Reviews.GetAllAsync(providerId);

            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpGet("avg-review")]
        public async Task<IActionResult> AverageReview([FromQuery] int? providerId)
        {
            var result = await _unitOfWork.Reviews.AverageReview(providerId);

            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
    }
}
