using Beanies.Models;
using Beanies.Models.BackendResponses;
using Beanies.Services.Backend.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beanies.Services.Backend
{
    class GameBackendService : AbstractBackendService, IGameBackendService
    {

        private string gamesUrl => $"{baseUrl}/games/";
        public async Task<Game> CreateGame(string name, List<string> players)
        {
            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                {"name",name },
                {"players", JsonConvert.SerializeObject(players) }
            };

            var res = await PostAsync(gamesUrl, body);
            if (!res.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var gameResponse = JsonConvert.DeserializeObject<GameResponse>(content);
                return gameResponse.game;
            }
        }

        public async Task<bool> DeleteGame(string id)
        {
            var deleteGameUrl = $"{gamesUrl}{id}";
            var res = await GetAsync(deleteGameUrl);
            if (!res.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var gameResponse = JsonConvert.DeserializeObject<GameResponse>(content);
                return gameResponse.success;
            }
        }

        public async Task<Game> GetGame(string id)
        {
            var getGamesUrl = $"{gamesUrl}{id}";
            var res = await GetAsync(getGamesUrl);
            if (!res.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var gameResponse = JsonConvert.DeserializeObject<GameResponse>(content);
                return gameResponse.game;
            }
        }

        public async Task<List<Game>> GetGamesSelf()
        {
            var res = await GetAsync(gamesUrl);
            if (!res.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var gameResponse = JsonConvert.DeserializeObject<GameResponse>(content);
                return gameResponse.games;
            }
        }

        public async Task<Game> UpdateGame(Game game)
        {
            var res = await PutAsync(gamesUrl, game);
            if (!res.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await res.Content.ReadAsStringAsync();
                var gameResponse = JsonConvert.DeserializeObject<GameResponse>(content);
                return gameResponse.game;
            }
        }
    }
}
