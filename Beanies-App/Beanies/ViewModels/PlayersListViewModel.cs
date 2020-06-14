using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Beanies.Models;
using Beanies.Services;
using Xamarin.Forms;

namespace Beanies.ViewModels
{
    public class PlayersListViewModel : BaseViewModel
    {
        public IDataStore<User> PlayerDataStore => DependencyService.Get<IDataStore<User>>();
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

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public Command<string> RemovePlayer { get; set; }


        public PlayersListViewModel()
        {
            RemovePlayer = new Command<string>(RemovePlayerAsync);
            FetchPlayers();
        }

        public async void AddPlayer()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                await PlayerDataStore.AddAsync(new User() { Name = Name}); //todo: FIX ME
            }
            Name = string.Empty;
            FetchPlayers();
        }

        public async void FetchPlayers()
        {
            Players = (await PlayerDataStore.GetAllAsync()).ToList();
        }

        async void RemovePlayerAsync(string id)
        {
            await PlayerDataStore.DeleteAsync(id);
            FetchPlayers();
        }
    }
}
