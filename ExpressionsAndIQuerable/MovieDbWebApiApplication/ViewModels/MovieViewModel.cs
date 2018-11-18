using System;

namespace MovieDbWebApiApplication.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double VoteAverage { get; set; }
    }
}