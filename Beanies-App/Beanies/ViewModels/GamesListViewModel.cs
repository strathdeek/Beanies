using System;
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
        public ObservableCollection<Game> Games { get; set; }
        public IDataStore<Game> GameDataStore => DependencyService.Get<IDataStore<Game>>();

        public GamesListViewModel()
        {
            FetchGames();
        }

        public async Task FetchGames()
        {
            Games = new ObservableCollection<Game>();
            var games = await GameDataStore.GetAllAsync();
            foreach (var game in games)
            {
                Games.Add(game);
            }
        }

        public string PlayIcon => IconFont.Cards;
        public string PlayersIcon => IconFont.AccountMultiple;
    }
}
