using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OneGate.Backend.Gateway.Base.Options;

namespace OneGate.Backend.Gateway.Base.Extensions.Authentication
{
    public static class AuthenticationBaseExtensions
    {
        public static IServiceCollection AddBaseAuthentication(this IServiceCollection services, AuthenticationOptions options)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecurityKey));
            services.AddAuthentication(p =>
                {
                    p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(p =>
                {
                    p.RequireHttpsMetadata = false;
                    p.SaveToken = true;
                    p.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "OneGate",
                        ValidateAudience = true,
                        ValidAudience = options.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = securityKey
                    };
                });
            return services;
        }
    }
}