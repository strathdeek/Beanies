using System;
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
            return new SQLiteAsyncConnection(Constants.GameDatabasePath, Constants.Flags);
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
    }
}
