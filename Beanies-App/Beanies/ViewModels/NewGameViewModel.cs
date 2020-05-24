using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beanies.Models;
using Beanies.Services;
using Xamarin.Forms;

namespace Beanies.ViewModels
{
    public class NewGameViewModel : BaseViewModel
    {
        public IDataStore<User> PlayerDataStore => DependencyService.Get<IDataStore<User>>();
        public IDataStore<Game> GameDataStore => DependencyService.Get<IDataStore<Game>>();
        public Command<object> OnPlayerSelectionChanged;

        public NewGameViewModel()
        {
            OnPlayerSelectionChanged = new Command<object>(PlayerSelectionChanged);
            NumberOfRounds = 13;
            FetchPlayers();
        }

        private int numberOfRounds;
        public int NumberOfRounds
        {
            get
            {
                return numberOfRounds;
            }
            set
            {
                numberOfRounds = value;
                OnPropertyChanged(nameof(NumberOfRounds));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            { 
                name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        private List<User> players;
        public List<User> Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
                OnPropertyChanged("Players");
            }
        }

        private List<User> selectedPlayers;
        public List<User> SelectedPlayers
        {
            get { return selectedPlayers; }
            set
            { 
                selectedPlayers = value;
                OnPropertyChanged(nameof(selectedPlayers));
            }
        }

        public async void FetchPlayers()
        {
            Players = (await PlayerDataStore.GetAllAsync()).ToList();
        }

        public async Task<bool> AddGameAsync()
        {
            if (!CanCreateGame())
            {
                return false;
            }
            else
            {
                var game = new Game();
                return await GameDataStore.AddAsync(game);
            }
        }

        private bool CanCreateGame()
        {
            return (!string.IsNullOrEmpty(Title) &&
               !(SelectedPlayers?.Count < 2));
        }

        public void PlayerSelectionChanged(object newSelection)
        {
            if (newSelection is IEnumerable<User> players)
                SelectedPlayers = players.ToList();
        }
    }
}
