using CarMaintainance.API.OptionsPattern;
using CarMaintenance.Infrastructre.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarMaintainance.API.JWTProvider
{
    public class JWTService(IOptions<JWTOptions> option) : IJWTService
    {
        private readonly IOptions<JWTOptions> _option = option;

        public (string token, int expiresIn) GenerateToken(ApplicationUser user)
        {
            // Cliams 
            Claim[] claims = [
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString() )

                ];

            var symmSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.Value.Key));

            var signInCredentails = new SigningCredentials(symmSecurityKey,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _option.Value.Issuer,
                audience: _option.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_option.Value.ExpiryMinutes),
                signingCredentials: signInCredentails);
            
            return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: _option.Value.ExpiryMinutes);
                
                
                
        }

        //public string? ValidateToken(string token)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    handler.ValidateToken{
        //        IssuerValidator  = 
        //    }

        //}
    }
}
