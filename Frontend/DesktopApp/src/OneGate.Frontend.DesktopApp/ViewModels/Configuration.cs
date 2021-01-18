using System;

namespace OneGate.Frontend.DesktopApp.ViewModels
{
    /// <summary>
    /// This is the temporary abstraction that is required before the implementation of DI.
    /// </summary>
    static class Configuration
    {
        public static string ClientKey { get; } = "4E645267556B58703272357538782F41";

        /// <summary>
        /// The server's ip address.
        /// </summary>
        public static Uri EndpointUri { get; } = new Uri("https://178.154.228.89/api/v1/");
    }
}