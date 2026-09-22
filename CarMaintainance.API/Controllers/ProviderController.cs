using CarMaintainance.API.JWTProvider;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.Services.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintainance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProviderController(IProviderService providerService , IProviderRepository providerRepository ) : ControllerBase
    {
        private readonly IProviderService _providerService = providerService;
        private readonly IProviderRepository _providerRepository = providerRepository;

        [HttpPost(template: "create")]
        public async Task<IActionResult> Create([FromBody] ProviderRequest request)
        {
            var result = await _providerRepository.AddAsync(request);

            return result.IsSuccess
                ? CreatedAtAction(nameof(GetById), new { id = result.ValueOuter!.Id }, result.ValueOuter)
                : result.ToProblem(400);
        }
        [HttpGet("get-all-activated-verified")]
        public async Task<IActionResult> GetAllActivatedVerified()
        {
            var result = await _providerRepository.GetAllActivatedVerifiedAsync();
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _providerRepository.GetAllAsync();
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpGet("{id:int}/get-by-id")]
        public async Task<IActionResult> GetById([FromRoute] int id )
        {
            var result = await _providerRepository.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpDelete("{id:int}/delte")]
        public async Task<IActionResult> Delete([FromRoute] int id )
        {
            var result = await _providerRepository.DeleteAsync(id);
            return result.IsSuccess ? Ok() : result.ToProblem(400);
        }


        [HttpPut("{id:int}/update")]
        public async Task<IActionResult> Update([FromRoute] int id  , [FromBody] ProviderRequest request)
        {
            var result = await _providerRepository.UpdateAsync(id,request);
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
            
        }
        //TODO  : Toggle Status [Active, Verify] , soft delete => isDeleted as a  col in Provider ? 


    }
}
