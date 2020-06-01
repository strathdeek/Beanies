using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beanies.Models;

namespace Beanies.Services
{
    public class MockUserDataStore : IDataStore<User>
    {
        readonly List<User> players;

        public MockUserDataStore()
        {
            players = new List<User>()
            {
                new User { Id = Guid.NewGuid().ToString(), Name = "Kevin", Guest = false},
                new User { Id = Guid.NewGuid().ToString(), Name = "Alex", Guest = true},
                new User { Id = Guid.NewGuid().ToString(), Name = "Elif", Guest = true }
            };
        }

        public async Task<bool> AddAsync(User item)
        {
            players.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(User item)
        {
            var oldItem = players.Where((User arg) => arg.Id == item.Id).FirstOrDefault();
            players.Remove(oldItem);
            players.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = players.Where((User arg) => arg.Id == id).FirstOrDefault();
            players.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<User> GetAsync(string id)
        {
            return await Task.FromResult(players.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(players);
        }
    }
}