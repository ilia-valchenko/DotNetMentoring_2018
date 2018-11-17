using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace QueryableProviderForMovieDb.Entities
{
    public class DiscoveryEntity : MovieDbEntity
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("results")]
        public List<DiscoverItemEntity> Results { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        public override string ToString()
        {
            var sBuilder = new StringBuilder($"Page: {Page}; Total results: {TotalResults}; Total pages: {TotalPages};{Environment.NewLine}");

            foreach (var discoveredItem in Results)
            {
                sBuilder.Append(discoveredItem);
                sBuilder.Append(Environment.NewLine);
            }

            return sBuilder.ToString();
        }
    }
}
