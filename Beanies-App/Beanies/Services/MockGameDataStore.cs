using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beanies.Models;
using Xamarin.Forms;

namespace Beanies.Services
{
    public class MockGameDataStore : IDataStore<Game>
    {
        readonly List<Game> games;
        public IDataStore<User> PlayerDataStore => DependencyService.Get<IDataStore<User>>();


        public MockGameDataStore()
        {
            var players =  PlayerDataStore.GetAllAsync().Result;
            var kevin = players.First(x => x.Name == "Kevin");
            var elif = players.First(x => x.Name == "Elif");
            var alex = players.First(x => x.Name == "Alex");

            List<User> playerSet1 = new List<User>() { kevin, alex };
            List<User> playerSet2 = new List<User>() { kevin, alex, elif };
            games = new List<Game>()
            {
                new Game()
                {
                    Id = new Guid().ToString(),
                    Name = "Kevin + Alex game",
                    Players = playerSet1,
                    CurrentRound = 5,
                    TotalRounds = 12,
                    ScoreBoard = new Dictionary<User, int>()
                    {
                        {kevin,27 },
                        {alex,50 }
                    }
                },
                new Game()
                {
                    Id = new Guid().ToString(),
                    Name = "Game with Elif",
                    Players = playerSet2,
                    CurrentRound = 9,
                    TotalRounds = 12,
                    ScoreBoard = new Dictionary<User, int>()
                    {
                        {kevin,109 },
                        {alex,58 },
                        {elif,20 }
                    }
                }
            };
        }

        public async Task<bool> AddAsync(Game item)
        {
            games.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = games.Where((Game arg) => arg.Id == id).FirstOrDefault();
            games.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Game> GetAsync(string id)
        {
            return await Task.FromResult(games.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Game>> GetAllAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(games);
        }

        public async Task<bool> UpdateAsync(Game item)
        {
            var oldItem = games.Where((Game arg) => arg.Id == item.Id).FirstOrDefault();
            games.Remove(oldItem);
            games.Add(item);
            return await Task.FromResult(true);
        }
    }
}
