using CarMaintenance.Application.Services;
using System.IdentityModel.Tokens.Jwt;


namespace CarMaintenance.Infrastructre.Implementations
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor): ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string UserId =>
     _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
     ?? throw new UnauthorizedAccessException();
    }
}
