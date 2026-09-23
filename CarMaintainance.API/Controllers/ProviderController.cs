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

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearbyPoint([FromQuery] double lat,[FromQuery] double lng,[FromQuery] double radiusKm,[FromQuery] ProviderCategory? category)
        {
            var result = await _providerService.GetNearbyProviders(lat, lng, radiusKm, category);
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpGet("{id:int}/get-by-id")]
        public async Task<IActionResult> GetById([FromRoute] int id )
        {
            var result = await _providerRepository.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpGet("{id}/get-prividersAssigned-to-owner")]
        public async Task<IActionResult> GetProvidersByOwner([FromRoute] string id )
        {
            var result = await _providerService.GetProvidersByOwner(id);
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpGet("get-grouped-provider-with-owner")]
        public async Task<IActionResult> GetProvidersGroupedByOwner()
        {
            var result = await _providerService.GetProvidersGroupedByOwner();
            return result.IsSuccess ? Ok(result.ValueOuter) : result.ToProblem(400);
        }
        [HttpDelete("{id:int}/delete")]
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

        [HttpPut("{id:int}/toggle")]
        public async Task<IActionResult> Toggle([FromRoute] int id, [FromQuery] bool? isActive , [FromQuery]  bool? isVerified, [FromQuery]  bool? isDeleted)
        {
            var result = await _providerService.ToggleStatus(id,isActive,isVerified,isDeleted);
            return result.IsSuccess ? NoContent() : result.ToProblem(400);
            
        }
        


    }
}
