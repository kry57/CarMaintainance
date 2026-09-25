using CarMaintenance.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CarMaintainance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpPost(template: "{providerId:int}/create")]
        public async Task<IActionResult> Create([FromRoute] int providerId, [FromBody] ProductRequest product)
        {
            var result = await _unitOfWork.Products.AddAsync(providerId, product);
            

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { providerId, id = result.ValueOuter!.Id }, result.ValueOuter)
                : result.ToProblem(400);
        }
        [HttpPost(template: "{providerId:int}/images/{productId}")]
        public async Task<IActionResult> AddImage([FromRoute] int providerId, [FromRoute] int productId, [FromForm] ProductImageRequest request)
        {
            var result = await _unitOfWork.Products.AddImageAsync(providerId, productId, request);
            

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { providerId, id = result.ValueOuter!.Id }, result.ValueOuter)
                : result.ToProblem(400);
        }
        [HttpPut(template: "{providerId:int}/update/{productId}")]
        public async Task<IActionResult> Update([FromRoute] int providerId , [FromRoute] int productId, [FromBody] ProductRequest product)
        { 
            var result = await _unitOfWork.Products.UpdateAsync(providerId, productId, product);


            return result.IsSuccess
                ? NoContent() : result.ToProblem(400);
        }
        [HttpPut(template: "{providerId:int}/toggleStatus/{productId}")]
        public async Task<IActionResult> ToggleStatus([FromRoute] int providerId , [FromRoute] int productId)
        { 
            var result = await _unitOfWork.Products.ToggleStatus(providerId, productId);


            return result.IsSuccess
                ? NoContent() : result.ToProblem(400);
        }

        [HttpGet(template: "get-all")]
        public async Task<IActionResult> GetAll([FromQuery] int? providerId)
        {
            var result = await _unitOfWork.Products.GetAllAsync(providerId);


            return result.IsSuccess
                ? Ok(result.ValueOuter) : result.ToProblem(400);
        }

        [HttpGet("{providerId}/get-by-id/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int providerId, [FromRoute] int id)
        {
            var result =await _unitOfWork.Products.GetByIdAsync(providerId,id);

            return result.IsSuccess ?   Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpDelete("{providerId}/delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int providerId, [FromRoute] int id)
        {
            var result =await _unitOfWork.Products.DeleteAsync(providerId,id);

            return result.IsSuccess ?   Ok() : result.ToProblem(400);
        }
        [HttpDelete("{providerId}/delete/{productId:int}/image/{imageId}")]
        public async Task<IActionResult> DeleteImage([FromRoute] int providerId, [FromRoute] int productId, [FromRoute] int imageId)
        {
            var result =await _unitOfWork.Products.DeleteImageAsync(providerId,productId, imageId);

            return result.IsSuccess ?   Ok() : result.ToProblem(400);
        }
        
    }
}
