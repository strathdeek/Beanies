using Beanies.Models;
using Beanies.Services.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Beanies.Services.Datastore
{
    class GameDataStore : IDataStore<Game>
    {
        
        private List<Game> Games;

        IGameBackendService GameBackendService => DependencyService.Resolve<IGameBackendService>();

        public GameDataStore()
        {
            Games = new List<Game>();
        }

        public async Task<bool> AddAsync(Game item)
        {
            var newGame = await GameBackendService.CreateGame(item.Name, item.Players);
            if (newGame != null)
            {
                if (Games == null)
                    Games = new List<Game>();
                Games.Add(newGame);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deletedSuccesfully = await GameBackendService.DeleteGame(id);
            var gameToRemove = Games.First(x => x.Id == id);
            if (deletedSuccesfully && gameToRemove!=null)
            {
                return Games.Remove(gameToRemove) && deletedSuccesfully;
            }
            return deletedSuccesfully;
        }

        public async Task<IEnumerable<Game>> GetAllAsync(bool forceRefresh = false)
        {
            if (!Games.Any())
            {
                Games = await GameBackendService.GetGamesSelf();
            }
            return Games;
        }

        public async Task<Game> GetAsync(string id)
        {
            if (!Games.Any())
            {
                Games = await GameBackendService.GetGamesSelf();
            }
            return Games.FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(Game item)
        {
            var updatedGame = await GameBackendService.UpdateGame(item);
            if (updatedGame == null) return false;
            var gameToUpdate = Games.First(x => x.Id == item.Id);
            var index = Games.IndexOf(gameToUpdate);
            Games[index] = updatedGame;
            return true;
        }
    }
}
