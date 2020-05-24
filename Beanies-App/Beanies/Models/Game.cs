using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Beanies.Models
{
    public class Game
    {
        public Game()
        {

        }

        [JsonProperty(PropertyName = "id")]
        public string Id;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "players")]
        public List<User> Players { get; set; }

        [JsonProperty(PropertyName = "scores")]
        public Dictionary<User, int[]> Scores { get; set; }

        [JsonIgnore]
        public int Rounds { 
            get
            {
                return 5;
            }

        }
    }
}
