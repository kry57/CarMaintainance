using CarMaintenance.Infrastructre.Identity;
using CarMaintenance.Infrastructre.OptionsPattern;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace CarMaintenance.Infrastructre.JWT
{
    public class JWTService(IOptions<JWTOptions> option) : IJWTService
    {
        private readonly IOptions<JWTOptions> _option = option;

        public (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> Roles, IEnumerable<string> Permissions)
        {
            // Cliams 
        Claim[] claims = [
        new(JwtRegisteredClaimNames.Sub, user.Id),
        new(JwtRegisteredClaimNames.Email, user.Email!),
        new(JwtRegisteredClaimNames.FamilyName, user.FullName),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new(nameof(Roles), JsonSerializer.Serialize(Roles), JsonClaimValueTypes.JsonArray),
        new(nameof(Permissions), JsonSerializer.Serialize(Permissions), JsonClaimValueTypes.JsonArray)
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

        public string? ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var symmSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.Value.Key));
            try
            {
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true ,
                    IssuerSigningKey = symmSecurityKey ,

                    ValidateAudience = false , 
                    ValidateIssuer = false , 

                    ValidateLifetime = false, 
                    ClockSkew = TimeSpan.Zero


                }, out SecurityToken validatedToken);

                var jwtSecurityToken = (JwtSecurityToken)validatedToken;
                return jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

            }
            catch (SecurityTokenException ) {
                return null;

            }

        }
    }
}
