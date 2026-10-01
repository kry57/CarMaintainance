using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintainance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpPost("create")]
        public async Task<IActionResult> Add(CarRequest request)
        {
            var result = await _unitOfWork.Cars.AddAsync(request);

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { carId = result.ValueOuter!.Id }, result.ValueOuter)
                : result.ToProblem(400);
        }
        [HttpDelete("delete/{carId}")]
        public async Task<IActionResult> Delete([FromRoute] int carId)
        {
            var result  = await _unitOfWork.Cars.DeleteAsync(carId);
            return result.IsSuccess ? NoContent() : result.ToProblem(400);

        }
        [HttpPut("togglestatus/{carId}")]
        public async Task<IActionResult> ToggleStatus([FromRoute] int carId)
        {
            var result  = await _unitOfWork.Cars.ToggleStatus(carId);
            return result.IsSuccess ? NoContent() : result.ToProblem(400);

        }
        [HttpPut("update/{carId}")]
        public async Task<IActionResult> Update([FromRoute] int carId  , [FromBody] CarRequest request)
        {
            var result  = await _unitOfWork.Cars.UpdateAsync(carId, request);
            return result.IsSuccess ? NoContent() : result.ToProblem(400);

        }
        [HttpGet("get-by-Id/{carId}")]
        public async Task<IActionResult> GetById([FromRoute] int carId)
        {
            var result = await _unitOfWork.Cars.GetByIdAsync(carId);
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400); 
        }
        [HttpGet("get-all-per-customer")]
        public async Task<IActionResult> GetAllAsyncPerCustomer()
        {
            var result = await _unitOfWork.Cars.GetAllAsyncPerCustomer();
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400); 
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _unitOfWork.Cars.GetAllAsync();
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400); 
        }
    }
}
