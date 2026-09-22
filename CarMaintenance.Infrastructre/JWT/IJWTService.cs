using CarMaintenance.Infrastructre.Identity;

namespace CarMaintenance.Infrastructre.JWT
{
    public interface IJWTService
    {
        (string token, int expiresIn) GenerateToken(ApplicationUser user);
       // string? ValidateToken(string token); 
    }
}
