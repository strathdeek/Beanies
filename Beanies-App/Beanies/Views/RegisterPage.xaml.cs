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
    public partial class RegisterPage : ContentPage
    {
        private RegisterViewModel vm => (RegisterViewModel)BindingContext;
        public RegisterPage()
        {
            InitializeComponent();
        }

        public async void Register(object sender, EventArgs e)
        {
            if (await vm.Register())
            {
                Application.Current.MainPage = new NavigationPage(new GamesListPage());
            }
        }

        public async void NavigateToLoginPage(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}