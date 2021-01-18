//using OneGate.Frontend.ApiLibrary;
using ReactiveUI;

namespace OneGate.Frontend.DesktopApp.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
        /// <summary>
        /// Reference to the base window of the 
        /// application where the controls are located.
        /// </summary>
        public IBaseWindow BaseWindow;

        /// <summary>
        /// Implements access to an instance of 
        /// the OneGateApi class (server API).
        /// </summary>
        //public OneGateApi ServerApi { get; set; }
    }
}
