using Xamarin.Forms;
using Beanies.Services;
using Beanies.Views;
using Beanies.Services.Backend.Interfaces;
using Beanies.Services.Backend;
using Beanies.Models;
using Beanies.Services.Datastore;
using System;
using System.Threading.Tasks;
using Beanies.Services.LocalDatabase;

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
            DependencyService.RegisterSingleton(new UserDatabase());
            DependencyService.RegisterSingleton(new GameDatabase());

            var sessionService = DependencyService.Resolve<ISessionService>();

            MainPage = sessionService.HasActiveSession()
                ? new NavigationPage(new GamesListPage())
                : new NavigationPage(new LoginPage());
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
