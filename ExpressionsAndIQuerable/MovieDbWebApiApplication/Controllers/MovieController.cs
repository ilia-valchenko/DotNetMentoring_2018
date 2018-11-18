using MovieDbWebApiApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace MovieDbWebApiApplication.Controllers
{
    public class MovieController : ApiController
    {
        private readonly List<MovieViewModel> _movies;

        public MovieController()
        {
            _movies = new List<MovieViewModel>
            {
                new MovieViewModel
                {
                    Id = 1,
                    Title = "Blade",
                    Overview = "When Blade's mother was bitten by a vampire during pregnancy, she did not know that she gave her son a special gift while dying: All the good vampire attributes in combination with the best human skills. Blade and his mentor Whistler battle an evil vampire rebel (Deacon Frost) who plans to take over the outdated vampire council, capture Blade and resurrect voracious blood god La Magra.",
                    ReleaseDate = new DateTime(1998, 8, 25),
                    VoteAverage = 6.5
                },
                new MovieViewModel
                {
                    Id = 2,
                    Title = "Blade Runner",
                    Overview = "In the smog-choked dystopian Los Angeles of 2019, blade runner Rick Deckard is called out of retirement to terminate a quartet of replicants who have escaped to Earth seeking their creator for a way to extend their short life spans.",
                    ReleaseDate = new DateTime(1982, 6, 25),
                    VoteAverage = 7.9
                },
                new MovieViewModel
                {
                    Id = 3,
                    Title = "Blade Runner 2049",
                    Overview = "Thirty years after the events of the first film, a new blade runner, LAPD Officer K, unearths a long-buried secret that has the potential to plunge what's left of society into chaos. K's discovery leads him on a quest to find Rick Deckard, a former LAPD blade runner who has been missing for 30 years.",
                    ReleaseDate = new DateTime(2017, 10, 04),
                    VoteAverage = 7.3
                },
                new MovieViewModel
                {
                    Id = 4,
                    Title = "First Man",
                    Overview = "A look at the life of the astronaut, Neil Armstrong, and the legendary space mission that led him to become the first man to walk on the Moon on July 20, 1969.",
                    ReleaseDate = new DateTime(2018, 10, 11),
                    VoteAverage = 7.2
                },
                new MovieViewModel
                {
                    Id = 5,
                    Title = "Iron Man",
                    Overview = "After being held captive in an Afghan cave, billionaire engineer Tony Stark creates a unique weaponized suit of armor to fight evil.",
                    ReleaseDate = new DateTime(2008, 4, 30),
                    VoteAverage = 7.5
                },
                new MovieViewModel
                {
                    Id = 6,
                    Title = "Spider-Man",
                    Overview = "After being bitten by a genetically altered spider, nerdy high school student Peter Parker is endowed with amazing powers.",
                    ReleaseDate = new DateTime(2002, 5, 1),
                    VoteAverage = 7
                }
            };
        }

        public List<MovieViewModel> GetMovies(string query)
        {
            var searchModel = JsonConvert.DeserializeObject<QueryModel>(query);
            var movies = _movies.ToList();

            foreach(var field in searchModel.Fields)
            {
                foreach(var movie in _movies)
                {
                    if(!IsMatchedProperty(movie, field))
                    {
                        movies.Remove(movie);
                    }
                }
            }

            return movies;
        }

        private bool IsMatchedProperty(MovieViewModel model, Field field)
        {
            switch(field.Name)
            {
                case nameof(MovieViewModel.Id):
                    int id;
                    Int32.TryParse(field.Value, out id);
                    return model.Id == id;

                case nameof(MovieViewModel.Title):
                    return model.Title.ToLower() == field.Value.ToLower();

                case nameof(MovieViewModel.Overview):
                    return model.Overview.ToLower() == field.Value.ToLower();

                case nameof(MovieViewModel.ReleaseDate):
                    DateTime releaseDate;
                    DateTime.TryParse(field.Value, out releaseDate);
                    return model.ReleaseDate == releaseDate;

                case nameof(MovieViewModel.VoteAverage):
                    double vote;
                    Double.TryParse(field.Value, out vote);
                    return model.VoteAverage == vote;

                default:
                    return false;
            }
        }
    }
}