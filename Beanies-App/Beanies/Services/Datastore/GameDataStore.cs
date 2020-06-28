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
                var newGameDb = new GameDb(newGame);
                await gameDatabase.SaveGameAsync(newGameDb);
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
                    var newGameDb = new GameDb(game);
                    await gameDatabase.SaveGameAsync(newGameDb);
                }
                return remoteGames;
            }
            return localGames.Select(x => new Game(x));
        }

        public async Task<Game> GetAsync(string id)
        {
            var localGames = await gameDatabase.GetGamesAsync();
            if (!localGames.Any())
            {
                var remoteGames = await GameBackendService.GetGamesSelf();
                foreach (var game in remoteGames)
                {
                    await gameDatabase.SaveGameAsync(new GameDb(game));
                }
                return remoteGames.FirstOrDefault(x => x.RemoteId == id);
            }
            return new Game(localGames.FirstOrDefault(x => x.RemoteId == id));
        }

        public async Task<bool> UpdateAsync(Game item)
        {
            var updatedGame = await GameBackendService.UpdateGame(item);
            if (updatedGame == null) return false;
            var gameToUpdate = await gameDatabase.GetGameByRemoteId(updatedGame.RemoteId);
            gameToUpdate.UpdateWith(updatedGame);
            await gameDatabase.SaveGameAsync(gameToUpdate);
            return true;
        }
    }
}
