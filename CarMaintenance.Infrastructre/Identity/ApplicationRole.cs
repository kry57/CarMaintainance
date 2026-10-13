using Microsoft.AspNetCore.Identity;

namespace CarMaintenance.Infrastructre.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsDefault { get; set; } 
        public bool IsDeleted { get; set; } 
    }
}
