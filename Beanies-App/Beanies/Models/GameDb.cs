using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace Beanies.Models
{
    public class GameDb
    {
        public GameDb()
        {
        }

        public GameDb(Game game)
        {
            UpdateWith(game);
        }

        public void UpdateWith(Game game)
        {
            RemoteId = game.RemoteId;
            Name = game.Name;
            CreatedDate = game.CreatedDate;
            Players = JsonConvert.SerializeObject(game.Players);
            Scores = JsonConvert.SerializeObject(game.Scores);
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string RemoteId { get; set; }

        public string Name { get; set; }

        public string Players { get; set; }

        public string Scores { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
