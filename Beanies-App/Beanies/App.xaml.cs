using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Beanies.Services;
using Beanies.Views;
using Beanies.Services.Backend.Interfaces;
using Beanies.Services.Backend;

namespace Beanies
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockUserDataStore>();
            DependencyService.Register<MockGameDataStore>();
            DependencyService.Register<IConfigurationService, ConfigurationService>();
            DependencyService.Register<ISessionService, SessionService>();
            DependencyService.Register<IUserService, UserService>();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
