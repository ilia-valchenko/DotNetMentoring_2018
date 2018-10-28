using Newtonsoft.Json;

namespace IQuerableTask1.E3SClient.Entities
{
    public class Recognition
    {
        [JsonProperty]
        public string nomination { get; set; }

        [JsonProperty]
        public string description { get; set; }

        [JsonProperty]
        public string recognitiondate { get; set; }

        [JsonProperty]
        public string points { get; set; }
    }
}
