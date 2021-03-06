using System.Reactive;
using System.Threading.Tasks;
//using OneGate.Frontend.ApiLibrary;
using ReactiveUI;

namespace OneGate.Frontend.DesktopApp.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        #region UserData

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = this.RaiseAndSetIfChanged(ref _email, value);
                CheckForEmptyForm();
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = this.RaiseAndSetIfChanged(ref _password, value);
                CheckForEmptyForm();
            }
        }

        #endregion

        private bool _isEnabledSignIn = false;

        /// <summary>
        /// Implements the logic of the authorization completion button.
        /// </summary>
        public bool IsEnabledSignIn
        {
            get => _isEnabledSignIn;
            set => _isEnabledSignIn = this.RaiseAndSetIfChanged(ref _isEnabledSignIn, value);
        }

        private bool _isFormEnabled = true;

        /// <summary>
        /// Implements verification of the correctness of filling out the authorization form.
        /// </summary>
        public bool IsFormEnabled
        {
            get => _isFormEnabled;
            set => this.RaiseAndSetIfChanged(ref _isFormEnabled, value);
        }

        private string _error;

        /// <summary>
        /// Implements the logic of the error message displayed in the UI.
        /// </summary>
        public string Error
        {
            get => _error;
            set => this.RaiseAndSetIfChanged(ref _error, value);
        }

        public ReactiveCommand<Unit, Unit> SignInCommand { get; }

        public ReactiveCommand<Unit, Unit> RegistrationCommand { get; }

        public SignInViewModel()
        {
            SignInCommand = ReactiveCommand.CreateFromTask(SignInAsync);
            RegistrationCommand = ReactiveCommand.CreateFromTask(SwitchToRegistrationAsync);
        }

        /// <summary>
        /// Implements access to the button to complete the authorization.
        /// </summary>
        private void CheckForEmptyForm()
            => IsEnabledSignIn = !(string.IsNullOrWhiteSpace(_email) 
                || string.IsNullOrWhiteSpace(_password));

        /// <summary>
        /// Implements user authorization.
        /// </summary>
        private async Task SignInAsync()
        {
            IsFormEnabled = false;
            /*
            try
            {
                var result = await OneGateApi.CreateTokenAsync(Configuration.EndpointUri, 
                    new OAuthDto
                    {
                        Username = Email,
                        Password = Password
                    }, 
                    new ClientKeyDto
                    {
                        ClientKey = Configuration.ClientKey
                    });
                var api = new OneGateApi(Configuration.EndpointUri, result.AccessToken);
                BaseWindow.Content = new MainViewModel(api);
            }
            catch (OneGateApiException e)
            {
                Error = e.Message;
                IsFormEnabled = true;
            }
            */
            BaseWindow.Content = new MainViewModel();
        }

        /// <summary>
        /// Implements the transition to the registration form.
        /// </summary>
        private async Task SwitchToRegistrationAsync()
            => BaseWindow.Content = new SignUpViewModel();
    }
}