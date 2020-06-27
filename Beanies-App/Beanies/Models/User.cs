using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SQLite;
using System;
namespace Beanies.Models
{
    public class User
    {
        public User()
        {
        }
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonProperty(propertyName: "_id")]
        public string RemoteId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "guest")]
        public bool Guest { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
