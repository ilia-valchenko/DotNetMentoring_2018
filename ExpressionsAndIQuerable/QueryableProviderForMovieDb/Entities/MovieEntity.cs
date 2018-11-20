using Newtonsoft.Json;
using System;

namespace QueryableProviderForMovieDb.Entities
{
    [JsonObject]
    public class MovieEntity : MovieDbEntity
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Overview")]
        public string Overview { get; set; }

        [JsonProperty("ReleaseDate")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("VoteAverage")]
        public double VoteAverage { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\nTitle: {Title}\nOverview: {Overview}\nRelease date: {ReleaseDate}\nAverage vote: {VoteAverage}";
        }
    }
}
