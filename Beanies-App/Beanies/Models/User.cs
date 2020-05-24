using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
namespace Beanies.Models
{
    public class User
    {
        public User()
        {
        }

        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "guest")]
        public bool Guest { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
