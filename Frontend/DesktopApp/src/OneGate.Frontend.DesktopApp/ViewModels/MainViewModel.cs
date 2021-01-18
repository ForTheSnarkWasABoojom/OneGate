using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using OneGate.Frontend.DesktopApp.ViewModels.Frames;
//using OneGate.Frontend.ApiLibrary;

namespace OneGate.Frontend.DesktopApp.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Reference to the changing content of the app window.
        /// </summary>
        private ViewModelBase _content;

        /// <summary>
        /// Implements getting access to the reference to 
        /// the changing content of the app window.
        /// </summary>
        public ViewModelBase Content
        {
            get => _content;
            set
            {
                _content = this;
                this.RaiseAndSetIfChanged(ref _content, value);
            }
        }

        public ReactiveCommand<Unit, Unit> GoTrading { get; }

        public ReactiveCommand<Unit, Unit> GoBots { get; }

        public ReactiveCommand<Unit, Unit> GoCash { get; }

        public ReactiveCommand<Unit, Unit> GoSettings { get; }

        public MainViewModel(/*OneGateApi serverApi*/)
        {
            ////ServerApi = serverApi;

            // Attention! Currently, all tasks do not asynchronous!
            //async Task OpenTradingContent() => Content = new TradingViewModel(ServerApi);
            async Task OpenTradingContent() => Content = new TradingViewModel();
            GoTrading = ReactiveCommand.CreateFromTask(OpenTradingContent);
            //async Task OpenBotsContent() => Content = new BotsViewModel(ServerApi);
            async Task OpenBotsContent() => Content = new BotsViewModel();
            GoBots = ReactiveCommand.CreateFromTask(OpenBotsContent);
            //async Task OpenCashContent() => Content = new CashViewModel(ServerApi);
            async Task OpenCashContent() => Content = new CashViewModel();
            GoCash = ReactiveCommand.CreateFromTask(OpenCashContent);
            //async Task OpenSettingsContent() => Content = new SettingViewModel(ServerApi);
            async Task OpenSettingsContent() => Content = new SettingViewModel();
            GoSettings = ReactiveCommand.CreateFromTask(OpenSettingsContent);
            
            // By default, the Trading content is displayed.
            //Content = new TradingViewModel(ServerApi);
            Content = new TradingViewModel();
        }
    }
}