using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beanies.ViewModels;
using Xamarin.Forms;

namespace Beanies.Views
{
    public partial class GamesListPage
    {
        GamesListViewModel viewModel => BindingContext as GamesListViewModel;
        public GamesListPage()
        {
            InitializeComponent();
        }

        async void StartGame_Tapped(System.Object sender, System.EventArgs e)
        {
            if (sender is ContentView view)
            {
                AnimateTap(view);
            }
            await Navigation.PushAsync(new NewGamePage());
        }

        async void Players_Tapped(System.Object sender, System.EventArgs e)
        {
            if (sender is ContentView view)
            {
                await AnimateTap(view);
            }
            await Navigation.PushModalAsync(new PlayersListPage());
        }

        private async Task AnimateTap(ContentView view)
        {
            await view.ScaleTo(.95, 75);
            await view.ScaleTo(1, 75);
        }
    }
}
