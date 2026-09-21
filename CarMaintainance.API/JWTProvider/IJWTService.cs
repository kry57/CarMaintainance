using CarMaintenance.Infrastructre.Identity;

namespace CarMaintainance.API.JWTProvider
{
    public interface IJWTService
    {
        (string token, int expiresIn) GenerateToken(ApplicationUser user);
       // string? ValidateToken(string token); 
    }
}
