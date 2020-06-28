using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Beanies.Models;
using Beanies.Services;
using Beanies.Styles;
using Beanies.ViewItems;
using Xamarin.Forms;

namespace Beanies.ViewModels
{
    public class GamesListViewModel : BaseViewModel
    {
        public IDataStore<Game> GameDataStore => DependencyService.Get<IDataStore<Game>>();

        public GamesListViewModel()
        {
            Games = new List<GameViewItem>();
            LoadingGames = false;
        }

        private List<GameViewItem> games;
        public List<GameViewItem> Games
        {
            get
            {
                return games;
            }
            set
            {
                games = value;
                OnPropertyChanged(nameof(Games));
            }
        }

        private bool loadingGames;
        public bool LoadingGames
        {
            get
            {
                return loadingGames;
            }
            set
            {
                loadingGames = value;
                OnPropertyChanged(nameof(LoadingGames));
            }
        }

        public Command FetchGamesCommand { get { return new Command((s) => FetchGames()); } }

        public async Task FetchGames()
        {
            LoadingGames = true;
            var games = (await GameDataStore.GetAllAsync()).Select(x => new GameViewItem(x)).ToList();
            foreach (var game in games)
            {
                await game.InitPlayerNamesString();
            }
            Games = games;
            LoadingGames = false;
        }

        public string PlayIcon => IconFont.Cards;
        public string PlayersIcon => IconFont.AccountMultiple;
    }
}
