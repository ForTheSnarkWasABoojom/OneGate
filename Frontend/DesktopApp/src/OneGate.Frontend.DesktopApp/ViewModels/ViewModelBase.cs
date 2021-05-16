using Microsoft.Extensions.Options;
using OneGate.Frontend.Client;
using ReactiveUI;

namespace OneGate.Frontend.DesktopApp.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
        /// <summary>
        /// Reference to the base window of the 
        /// application where the controls are located.
        /// </summary>
        public IBaseWindow BaseWindow { get; set; }

        /// <summary>
        /// Client options for connecting to the server.
        /// </summary>
        public IOptions<OneGateClientOptions> ConnectionOptions { get; set; }

        public OneGateClientSession ClientSession { get; set; }
    }
}