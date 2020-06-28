using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SQLite;
using System;
using System.Collections.Generic;

namespace Beanies.Models
{
    public class Game
    {
        public Game()
        {
        }

        public Game(GameDb game)
        {
            RemoteId = game.RemoteId;
            Name = game.Name;
            CreatedDate = game.CreatedDate;
            Players = JsonConvert.DeserializeObject<string[]>(game.Players);
            Scores = JsonConvert.DeserializeObject<Dictionary<string, int[]>>(game.Scores);
        }

        [JsonProperty(PropertyName = "_id")]
        public string RemoteId;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "players")]
        public string[] Players { get; set; }

        [JsonProperty(PropertyName = "scores")]
        public Dictionary<string, int[]> Scores { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime CreatedDate { get; set; }

    }
}
