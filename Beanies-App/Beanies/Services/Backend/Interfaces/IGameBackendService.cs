using Beanies.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beanies.Services.Backend.Interfaces
{
    interface IGameBackendService
    {
        Task<Game> CreateGame(string name, string[] players);

        Task<Game> GetGame(string id);

        Task<List<Game>> GetGamesSelf();

        Task<Game> UpdateGame(Game game);

        Task<bool> DeleteGame(string id);

    }
}
