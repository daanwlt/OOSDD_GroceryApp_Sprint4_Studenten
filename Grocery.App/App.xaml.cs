using Grocery.App.ViewModels;
using Grocery.App.Views;

namespace Grocery.App
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(LoginViewModel viewModel)
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new LoginView(viewModel);
        }
    }
}
