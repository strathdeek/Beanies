using System;
using System.IO;

namespace Beanies.Resources
{
    public static class Constants
    {
        private static string BasePath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public const string UserDatabaseFilename = "UserDatabase.db3";
        public const string GameDatabaseFilename = "GameDatabase.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string UserDatabasePath
        {
            get
            {
                return Path.Combine(BasePath, UserDatabaseFilename);
            }
        }

        public static string GameDatabasePath
        {
            get
            {
                return Path.Combine(BasePath, GameDatabaseFilename);
            }
        }
    }
}
