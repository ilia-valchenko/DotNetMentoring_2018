using System;

namespace QueryableProviderForMovieDb.Entities
{
    public class MovieEntity : MovieDbEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double VoteAverage { get; set; }
    }
}
