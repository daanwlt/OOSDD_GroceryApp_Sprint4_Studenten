
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Application.Interfaces.Services;
using Grocery.Domain.Entities;
using Grocery.App.Services;

namespace Grocery.App.ViewModels
{
    /// <summary>
    /// ViewModel responsible for managing login functionality and UI state.
    /// Follows Single Responsibility Principle by having only one responsibility:
    /// managing login UI state and authentication flow.
    /// </summary>
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly GlobalViewModel _global;
        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string loginMessage;

        public LoginViewModel(IAuthService authService, GlobalViewModel global, NavigationService navigationService)
        {
            _authService = authService;
            _global = global;
            _navigationService = navigationService;
        }

        /// <summary>
        /// Handles login attempt by authenticating user and updating UI state.
        /// This method has a single responsibility: managing login flow.
        /// </summary>
        [RelayCommand]
        private async Task Login()
        {
            Client? authenticatedClient = _authService.Login(Email, Password);
            
            if (authenticatedClient != null)
            {
                await HandleSuccessfulLogin(authenticatedClient);
            }
            else
            {
                HandleFailedLogin();
            }
        }

        /// <summary>
        /// Handles successful login by updating global state and navigating.
        /// This method has a single responsibility: successful login handling.
        /// </summary>
        /// <param name="authenticatedClient">The authenticated client</param>
        private async Task HandleSuccessfulLogin(Client authenticatedClient)
        {
            LoginMessage = $"Welkom {authenticatedClient.Name}!";
            _global.Client = authenticatedClient;
            await _navigationService.NavigateToMainApp();
        }

        /// <summary>
        /// Handles failed login by updating UI message.
        /// This method has a single responsibility: failed login handling.
        /// </summary>
        private void HandleFailedLogin()
        {
            LoginMessage = "Ongeldige inloggegevens.";
        }
    }
}
