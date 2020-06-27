using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beanies.Models;
using Beanies.Services;
using Beanies.Services.Backend.Interfaces;
using Beanies.Styles;
using Beanies.ViewItems;
using Xamarin.Forms;

namespace Beanies.ViewModels
{
    public class NewGameViewModel : BaseViewModel
    {
        public IDataStore<User> PlayerDataStore => DependencyService.Get<IDataStore<User>>();
        public IDataStore<Game> GameDataStore => DependencyService.Get<IDataStore<Game>>();
        private ISessionService sessionService => DependencyService.Get<ISessionService>();

        public Command<object> OnPlayerSelectionChanged;

        public NewGameViewModel()
        {
            FetchPlayers();
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

        private List<PlayerViewItem> players;
        public List<PlayerViewItem> Players
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

        public string CloseIcon => IconFont.Close;

        public async void FetchPlayers()
        {
            var users = await PlayerDataStore.GetAllAsync();
            Players = users.Select(x => new PlayerViewItem(x)
            {
                Selected = sessionService.Self == x.RemoteId,
                SelectedCommand = new Command<string>(SelectPlayer)
            }).ToList();
            var selectedIds = Players.Where(p => p.Selected).Select(x => x.Id);
            SelectedPlayers = users.Where(x => selectedIds.Any(y => y == x.RemoteId)).ToList();
        }

        public async Task<bool> AddGameAsync()
        {
            if (!CanCreateGame())
            {
                return false;
            }
            else
            {
                var game = new Game()
                {
                    Name = Name,
                    Players = selectedPlayers.Select(x => x.RemoteId).ToList()
                };
                return await GameDataStore.AddAsync(game);
            }
        }

        private bool CanCreateGame()
        {
            return (!string.IsNullOrEmpty(Name) &&
               !(SelectedPlayers?.Count < 2));
        }

        public void PlayerSelectionChanged(object newSelection)
        {
            if (newSelection is IEnumerable<User> players)
                SelectedPlayers = players.ToList();
        }

        private async void SelectPlayer(string id)
        {
            Players.FirstOrDefault(x => x.Id == id).ToggleSelected();
            var user = await PlayerDataStore.GetAsync(id);
            if (!selectedPlayers.Any(x => x.RemoteId == id))
            {
                selectedPlayers.Add(user);
            }
            else
            {
                selectedPlayers.Remove(user);
            }
            Players = Players.ToList();
        }

    }
}
