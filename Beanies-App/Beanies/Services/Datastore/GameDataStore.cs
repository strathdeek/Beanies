using Beanies.Models;
using Beanies.Services.Backend.Interfaces;
using Beanies.Services.LocalDatabase;
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
        
        IGameBackendService GameBackendService => DependencyService.Resolve<IGameBackendService>();
        GameDatabase gameDatabase => DependencyService.Resolve<GameDatabase>();

        public GameDataStore()
        {
        }

        public async Task<bool> AddAsync(Game item)
        {
            var newGame = await GameBackendService.CreateGame(item.Name, item.Players);
            if (newGame != null)
            {
                await gameDatabase.SaveGameAsync(newGame);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deletedSuccesfully = await GameBackendService.DeleteGame(id);
            var gameToRemove = await gameDatabase.GetGameByRemoteId(id);
            if (deletedSuccesfully && gameToRemove!=null)
            {
                await gameDatabase.DeleteGameAsync(gameToRemove);
            }
            return deletedSuccesfully;
        }

        public async Task<IEnumerable<Game>> GetAllAsync(bool forceRefresh = false)
        {
            var localGames = await gameDatabase.GetGamesAsync();
            if (!localGames.Any())
            {
                var remoteGames = await GameBackendService.GetGamesSelf();
                foreach (var game in remoteGames)
                {
                    await gameDatabase.SaveGameAsync(game);
                }
                return remoteGames;
            }
            return localGames;
        }

        public async Task<Game> GetAsync(string id)
        {
            var localGames = await gameDatabase.GetGamesAsync();
            if (!localGames.Any())
            {
                var remoteGames = await GameBackendService.GetGamesSelf();
                foreach (var game in remoteGames)
                {
                    await gameDatabase.SaveGameAsync(game);
                }
                localGames = remoteGames;
            }
            return localGames.FirstOrDefault(x => x.RemoteId == id);
        }

        public async Task<bool> UpdateAsync(Game item)
        {
            var updatedGame = await GameBackendService.UpdateGame(item);
            if (updatedGame == null) return false;
            await gameDatabase.SaveGameAsync(item);
            return true;
        }
    }
}
