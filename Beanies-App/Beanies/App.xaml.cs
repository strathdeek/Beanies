using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Beanies.Services;
using Beanies.Views;
using Beanies.Services.Backend.Interfaces;
using Beanies.Services.Backend;
using Beanies.Models;
using Beanies.Services.Datastore;

namespace Beanies
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IConfigurationService, ConfigurationService>();
            DependencyService.Register<ISessionService, SessionService>();
            DependencyService.Register<IUserBackendService, UserBackendService>();
            DependencyService.Register<IGameBackendService, GameBackendService>();
            DependencyService.Register<IDataStore<User>, PlayerDataStore>();
            DependencyService.Register<IDataStore<Game>, GameDataStore>();

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
