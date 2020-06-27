using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beanies.Extensions;
using Beanies.Models;
using Beanies.Resources;
using SQLite;

namespace Beanies.Services.LocalDatabase
{
    public class GameDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public GameDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Game).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Game)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Game>> GetGamesAsync()
        {
            return Database.Table<Game>().ToListAsync();
        }

        public Task<Game> GetGameAsync(int id)
        {
            return Database.Table<Game>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<Game> GetGameByRemoteId(string remoteId)
        {
            return Database.Table<Game>().Where(u => u.RemoteId == remoteId).FirstOrDefaultAsync();
        }

        public Task<int> SaveGameAsync(Game item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteGameAsync(Game item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
