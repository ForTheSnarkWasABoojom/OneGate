using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace OneGate.Backend.Gateway.Middleware
{
    public static class AuthPolicy
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static string ClientKey { get; } = Environment.GetEnvironmentVariable("API_CLIENT_KEY");
        public static SymmetricSecurityKey SecurityKey { get; } =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")));

        public static TimeSpan ExpirationSpan { get; } = TimeSpan.FromDays(1);

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }
    }
}