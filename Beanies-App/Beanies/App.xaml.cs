using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Beanies.Services;
using Beanies.Views;

namespace Beanies
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockUserDataStore>();
            DependencyService.Register<MockGameDataStore>();
            MainPage = new NavigationPage(new GamesListPage());
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
