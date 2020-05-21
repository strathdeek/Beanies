using System;
namespace Beanies.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string name)
        {
            Name = name;
            Id = Guid.NewGuid().ToString();
            GamesPlayed = 0;
            Wins = 0;
        }

        public string Name { get; set; }
        public string Id { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public double WinRate
        {
            get
            {
                if (GamesPlayed != 0)
                {
                    return ((double)Wins / GamesPlayed);
                }
                else return 0;
            }
        }

    }
}
