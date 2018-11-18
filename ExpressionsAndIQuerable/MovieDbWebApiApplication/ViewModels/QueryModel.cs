using Newtonsoft.Json;
using System.Collections.Generic;

namespace MovieDbWebApiApplication.ViewModels
{
    [JsonObject]
    public class QueryModel
    {
        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }
    }
}