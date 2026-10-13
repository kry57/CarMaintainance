using CarMaintenance.Infrastructre.JWT;
using Microsoft.AspNetCore.Identity;

namespace CarMaintenance.Infrastructre.Identity
{
    public  class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public ICollection<Provider> Providers { get; set; } = [];
        public ICollection<Car> Cars{ get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<RegistrationRequest> Registrations { get; set; } = [];
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
