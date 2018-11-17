using Newtonsoft.Json;

namespace QueryableProviderForMovieDb.Entities
{
    public class DiscoverItemEntity : MovieDbEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        public override string ToString()
        {
            return $"\nId: {Id};\nTitle: {Title};\nOverview: {Overview};\nRelease data: {ReleaseDate};\nVote average: {VoteAverage}";
        }
    }
}
