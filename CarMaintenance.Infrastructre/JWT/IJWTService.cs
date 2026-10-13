using CarMaintenance.Infrastructre.Identity;

namespace CarMaintenance.Infrastructre.JWT
{
    public interface IJWTService
    {
        (string token, int expiresIn) GenerateToken(ApplicationUser user , IEnumerable<string> Roles , IEnumerable<string> Permissions );
       string? ValidateToken(string token); 
    }
}
