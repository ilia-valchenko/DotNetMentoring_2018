using System;
using QueryableProviderForMovieDb;
using QueryableProviderForMovieDb.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleApplicationForQueryableProviderForMovieDb
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("\nEnter you query to start search movies:");
            //string query = Console.ReadLine();

            //var movieDbClient = new MovieDbQueryClient();
            //var discoverResult = movieDbClient.SearchMovies(query);

            //Console.WriteLine($"\nSearch results:\n{discoverResult}");

            // -----------------------------------------------------------

            //var client = new MovieDbQueryClient();
            //var linqProvider = new MovieDbLinqProvider(client);
            //MovieDbQuery<MovieEntity> movieQuery = new MovieDbQuery<MovieEntity>(null, linqProvider);
            //var result = movieQuery.Where(m => m.Title == "Blade runner");

            var movies = new MovieDbEntitySet<MovieEntity>();
            var filteredMovies = movies.Where(m => m.Id == 2 && m.Title == "Blade runner" && m.VoteAverage == 7.9);
            var result = filteredMovies.ToList();

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}
