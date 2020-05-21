using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beanies.ViewModels;
using Xamarin.Forms;

namespace Beanies.Views
{
    public partial class NewGamePage : ContentPage
    {
        NewGameViewModel viewModel => BindingContext as NewGameViewModel;
        public NewGamePage()
        {
            InitializeComponent();
        }

        public async void GoBack(object sender, EventArgs eventArgs)
        {
            await BackButton.ScaleTo(.75, 75);
            await BackButton.ScaleTo(1, 75);
            await Navigation.PopAsync();
        }

        private async Task AnimateTap(ContentView view)
        {
            await view.ScaleTo(.95, 75);
            await view.ScaleTo(1, 75);
        }

        public async void StartGame(object sender, EventArgs e)
        {
            if (sender is ContentView view)
            {
                await AnimateTap(view);
            }
            if (await viewModel.AddGameAsync())
            {
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Unable to Create Game", "Please enter a title and select at least 3 players", "OK");
            }
        }
    }
}
