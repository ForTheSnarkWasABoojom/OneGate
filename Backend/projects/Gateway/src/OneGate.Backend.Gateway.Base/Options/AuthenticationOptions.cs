namespace OneGate.Backend.Gateway.Base.Options
{
    public class AuthenticationOptions
    {
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public int ExpirationHours { get; set; }
        public string ClientFingerprint { get; set; }
    }
}