using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Beanies.Models;
using Beanies.Services;
using Beanies.Styles;
using Xamarin.Forms;

namespace Beanies.ViewModels
{
    public class GamesListViewModel : BaseViewModel
    {

        private List<Game> games;
        public List<Game> Games
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

        public IDataStore<Game> GameDataStore => DependencyService.Get<IDataStore<Game>>();

        public GamesListViewModel()
        {
            Games = new List<Game>();
        }

        public async Task FetchGames()
        {
            Games.Clear();
            var games = await GameDataStore.GetAllAsync();
            foreach (var game in games)
            {
                Games.Add(game);
            }
            OnPropertyChanged(nameof(Games));
        }

        public string PlayIcon => IconFont.Cards;
        public string PlayersIcon => IconFont.AccountMultiple;
    }
}
