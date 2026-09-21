using AutoMapper;
using CarMaintenance.Application.Abstractions;
using CarMaintenance.Application.DTOs.Request;
using CarMaintenance.Application.Services.Interfaces;
using CarMaintenance.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

namespace CarMaintainance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController(IUnitOfWork unitOfWork , IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ProviderRequest request)
        {
            if (request == null)
                return BadRequest(Result.Failure(new Error("InvalidData","Check the Provider Data")));
            var provider = _mapper.Map<Provider>(request);
            await _unitOfWork.Providers.AddAsync(provider);
            return Ok();

        }
    }
}
