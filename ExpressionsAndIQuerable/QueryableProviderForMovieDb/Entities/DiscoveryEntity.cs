using Newtonsoft.Json;
using System.Collections.Generic;

namespace QueryableProviderForMovieDb.Entities
{
    public class DiscoveryEntity
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("results")]
        public List<DiscoverItemEntity> Results { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }
}
