using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OneGate.Backend.Gateway.Base.Options;

namespace OneGate.Backend.Gateway.Base.Authentication
{
    public static class JwtBuilder
    {
        public static JwtSecurityToken FromCredentials(AuthenticationOptions options, int accountId)
        {
            var expirationSpan = TimeSpan.FromHours(options.ExpirationHours);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecurityKey));
            
            return new JwtSecurityToken(
                issuer: "OneGate",
                audience: options.Audience,
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
                },
                expires: DateTime.Now + expirationSpan,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}