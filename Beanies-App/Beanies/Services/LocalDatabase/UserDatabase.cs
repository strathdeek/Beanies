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
    public class UserDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public UserDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }
        


        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(User).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(User)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<User>> GetUsersAsync()
        {
            return Database.Table<User>().ToListAsync();
        }

        public Task<List<User>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<User>("SELECT * FROM [User] WHERE [Done] = 0");
        }

        public Task<User> GetUserAsync(int id)
        {
            return Database.Table<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<User> GetUserByRemoteId(string remoteId)
        {
            return Database.Table<User>().Where(u => u.RemoteId == remoteId).FirstOrDefaultAsync();
        }

        public Task<User> GetGuestUsers()
        {
            return Database.Table<User>().Where(u => u.Guest == true).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(User item)
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

        public Task<int> DeleteUserAsync(User item)
        {
            return Database.DeleteAsync(item);
        }

    }
}
