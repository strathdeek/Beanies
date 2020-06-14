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

        [JsonProperty(PropertyName = "_id")]
        public string Id;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "players")]
        public List<string> Players { get; set; }

        [JsonProperty(PropertyName = "scores")]
        public Dictionary<string, int[]> Scores { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime CreatedDate { get; set; }

    }
}
