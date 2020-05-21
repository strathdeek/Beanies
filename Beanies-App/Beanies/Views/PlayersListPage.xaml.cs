using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beanies.ViewModels;
using Xamarin.Forms;

namespace Beanies.Views
{
    public partial class PlayersListPage : ContentPage
    {
        PlayersListViewModel viewModel;
        public PlayersListPage()
        {
            InitializeComponent();
            viewModel = BindingContext as PlayersListViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.FetchPlayers();
        }

        async void AddNewPlayer(System.Object sender, System.EventArgs e)
        {
            if(sender is ContentView view)
            {
                await AnimateTap(view);
            }
            viewModel.AddPlayer();
        }

        async void RemovePlayer(object s, EventArgs e)
        {
            if (s is ContentView view)
            {
                await AnimateTap(view);
            }
        }

        async void Close(System.Object sender, System.EventArgs e)
        {
            await BackButton.ScaleTo(.75, 75);
            await BackButton.ScaleTo(1, 75);
            await Navigation.PopModalAsync();
        }

        private async Task AnimateTap(ContentView view)
        {
            await view.ScaleTo(.95, 75);
            await view.ScaleTo(1, 75);
        }
    }
}
