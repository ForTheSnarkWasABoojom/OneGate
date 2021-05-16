using System;
using System.Linq;
using System.Reactive;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OneGate.Backend.Gateway.User.Api.Contracts.Account;
using OneGate.Frontend.Client;
using ReactiveUI;

namespace OneGate.Frontend.DesktopApp.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        #region UserData

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                ClearErrorForm();
                _email = this.RaiseAndSetIfChanged(ref _email, value);
                if(!IsValidEmail(_email))
                {
                    ShowErrorMessage("Invalid email");
                }
                ValidateForm();
            }
        }

        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                ClearErrorForm();
                _firstName = this.RaiseAndSetIfChanged(ref _firstName, value);
                if (_firstName.Length < 1 || _firstName.Length > 30)
                {
                    ShowErrorMessage("Length of first name must be in range [1; 30]");
                }
                else if (CheckForDigits(_firstName))
                {
                    ShowErrorMessage("First name must not have digits");
                }
                else if (!IsValidName(_firstName))
                {
                    ShowErrorMessage("Invalid first name");
                }
                ValidateForm();
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                ClearErrorForm();
                _lastName = this.RaiseAndSetIfChanged(ref _lastName, value);
                if (_lastName.Length < 1 || _lastName.Length > 30)
                {
                    ShowErrorMessage("Length of last name must be in range [1; 30]");
                }
                else if (CheckForDigits(_lastName))
                {
                    ShowErrorMessage("Last name must not have digits");
                }
                else if (!IsValidName(_lastName))
                {
                    ShowErrorMessage("Invalid last name");
                }
                ValidateForm();
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                ClearErrorForm();
                _password = this.RaiseAndSetIfChanged(ref _password, value);
                if (!IsValidPassword(_password))
                {
                    ShowErrorMessage("Length of password must be in range [6; 30]");
                }
                ValidateForm();
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                ClearErrorForm();
                _confirmPassword = this.RaiseAndSetIfChanged(ref _confirmPassword, value);
                if (Password != _confirmPassword)
                {
                    ShowErrorMessage("The password is not confirmed.");
                }
                ValidateForm();
            }
        }

        #endregion

        private bool _isEnabledSignUp;

        /// <summary>
        /// Implements the logic of the registration completion button.
        /// </summary>
        public bool IsEnabledSignUp
        {
            get => _isEnabledSignUp;
            set => _isEnabledSignUp = this.RaiseAndSetIfChanged(ref _isEnabledSignUp, value);
        }

        /// <summary>
        /// Implements verification of the correctness of filling out the registration form.
        /// </summary>
        void ValidateForm()
            => IsEnabledSignUp = IsValidEmail(Email) && IsValidPassword(Password)
                && Password == ConfirmPassword && IsValidName(FirstName) && IsValidEmail(LastName);

        private bool _isFormEnabled = true;

        /// <summary>
        /// Implements the logic to access the fields of the registration.
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

        public ReactiveCommand<Unit, Unit> RegisterCommand { get; }

        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public SignUpViewModel(IOptions<OneGateClientOptions> options)
        {
            ConnectionOptions = options;
            RegisterCommand = ReactiveCommand.CreateFromTask(RegisterAsync);
            BackCommand = ReactiveCommand.CreateFromTask(SwitchToAuthorizationAsync);
        }

        /// <summary>
        /// Implements validation of the email address.
        /// </summary>
        bool IsValidEmail(string strIn)
            => strIn != null && Regex.IsMatch(strIn, 
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        /// <summary>
        /// Checks for the presence of digits in the string.
        /// </summary>
        private bool CheckForDigits(string name)
            => name.Any(char.IsDigit);

        /// <summary>
        /// Implements validation of the name.
        /// </summary>
        private bool IsValidName(string name)
            => Regex.IsMatch(name, @"^[\p{L}\p{M}' \.\-]+$");

        /// <summary>
        /// Implements validation of the password.
        /// </summary>
        private bool IsValidPassword(string password)
            => password != null && password.Length >= 6 && password.Length <= 30;

        /// <summary>
        /// Implements the registration of a new user.
        /// </summary>
        private async Task RegisterAsync()
        {
            IsFormEnabled = false;
            FirstName = RefactorNameAccordingToTheStandard(FirstName);
            LastName = RefactorNameAccordingToTheStandard(LastName);
            try
            {
                var client = new OneGateClient(ConnectionOptions);
                var model = new CreateAccountRequest()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password
                };
                await client.CreateAccountAsync(model);
                var session = await SignInViewModel.SignInAsync(ConnectionOptions, Email, Password);
                BaseWindow.Content = new MainViewModel(ConnectionOptions, session);
            }
            catch (Exception e)
            {
                ShowErrorMessage(e.Message);
            }
        }

        /// <summary>
        /// Universal method of refactoring a name according to the standard: 
        /// the first letter is always uppercase, the rest are lowercase.
        /// </summary>
        private string RefactorNameAccordingToTheStandard(string name)
        {
            if (name == null)
            {
                name = "";
            }
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }

        /// <summary>
        /// Display the reason of the error.
        /// </summary>
        private void ShowErrorMessage(string errorMessage)
        {
            Error = errorMessage;
            IsFormEnabled = true;
        }

        /// <summary>
        /// Clears the error line.
        /// </summary>
        private void ClearErrorForm()
            => ShowErrorMessage("");

        /// <summary>
        /// Switch to the sign in form.
        /// </summary>
        private async Task SwitchToAuthorizationAsync()
            => BaseWindow.Content = new SignInViewModel(ConnectionOptions);
    }
}