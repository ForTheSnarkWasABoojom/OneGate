using System;

namespace OneGate.Backend.Gateway.Middleware
{
    public class AuthCredentials : IAuthCredentials
    {
        public string ClientKey { get; } = Environment.GetEnvironmentVariable("API_CLIENT_KEY");
    }
}