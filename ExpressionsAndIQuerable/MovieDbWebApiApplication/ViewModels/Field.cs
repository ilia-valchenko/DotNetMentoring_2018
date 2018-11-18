using Newtonsoft.Json;

namespace MovieDbWebApiApplication.ViewModels
{
    [JsonObject]
    public class Field
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}