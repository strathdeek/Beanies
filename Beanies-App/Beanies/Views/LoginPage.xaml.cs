using Beanies.Services.Backend.Interfaces;
using Beanies.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beanies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel vm => BindingContext as LoginViewModel;
        public LoginPage()
        {
            InitializeComponent();
        }


        private async void Login(object sender, EventArgs e)
        {
            if (await vm.LoginAsync())
            {
                App.Current.MainPage = new NavigationPage(new GamesListPage());
            }
        }

        private async void NavigateToRegistrationPage(object s, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}