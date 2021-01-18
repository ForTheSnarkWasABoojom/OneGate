namespace OneGate.Frontend.DesktopApp.ViewModels
{
    public interface IBaseWindow
    {
        /// <summary>
        /// Implements getting access to the reference to 
        /// the changing content of the app window.
        /// </summary>
        public ViewModelBase Content { get; set; }
    }
}