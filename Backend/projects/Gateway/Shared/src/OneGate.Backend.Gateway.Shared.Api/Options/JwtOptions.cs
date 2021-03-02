namespace OneGate.Backend.Gateway.Shared.Api.Options
{
    public class JwtOptions
    {
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public int ExpirationHours { get; set; }
        public string ClientFingerprint { get; set; }
    }
}