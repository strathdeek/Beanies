using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beanies.Models;
using Beanies.Services;
using Xamarin.Forms;

namespace Beanies.ViewItems
{
    public class GameViewItem
    {
        IDataStore<User> userDataStore = DependencyService.Resolve<IDataStore<User>>();

        public GameViewItem()
        {
        }
        public GameViewItem(Game game)
        {
            RemoteId = game.RemoteId;
            Name = game.Name;
            Players = game.Players;
            CreatedDate = game.CreatedDate;
            Scores = game.Scores;
        }

        public async Task InitPlayerNamesString()
        {
            Player1Name = (await userDataStore.GetAsync(Players[0])).Name;
            Player2Name = (await userDataStore.GetAsync(Players[1])).Name;
            Player3Name = (await userDataStore.GetAsync(Players[2])).Name;
            ExtraPlayersText = Players.Length > 3
                ? $"+ {Players.Length-3} . . ."
                : string.Empty;
        }

        public Command<string> SelectedCommand { get; set; }

        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public string Player3Name { get; set; }
        public string ExtraPlayersText { get; set; }

        public string RemoteId { get; set; }
        public string Name { get; set; }
        public string[] Players { get; set; }
        public DateTime CreatedDate { get; set; }
        public Dictionary<string,int[]> Scores { get; set; }
    }
}
