namespace OneGate.Frontend.Client
{
    public class OneGateClientSession
    {
        public string AccessToken { get; set; }

        public OneGateClientSession() { }

        public OneGateClientSession(string token)
            => AccessToken = token;
    }
}