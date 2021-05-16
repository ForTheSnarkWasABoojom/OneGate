using System;

namespace OneGate.Frontend.Client
{
    public class OneGateClientOptions
    {
        public Uri BaseUri { get; }

        public string ClientKey { get; }

        public OneGateClientOptions() { }

        public OneGateClientOptions(Uri uri, string key)
        {
            BaseUri = uri;
            ClientKey = key;
        }
    }
}