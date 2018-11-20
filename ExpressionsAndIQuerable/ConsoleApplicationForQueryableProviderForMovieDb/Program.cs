using System;
using QueryableProviderForMovieDb;
using QueryableProviderForMovieDb.Entities;
using System.Linq;

namespace ConsoleApplicationForQueryableProviderForMovieDb
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = new MovieDbEntitySet<MovieEntity>();

            // 1
            //var filteredMovies = movies.Where(m => m.Id == 2 && m.Title == "Blade runner" && m.VoteAverage == 7.9);

            // 2
            var filteredMovies = movies.Where(m => m.Id == 5);

            foreach (var movie in filteredMovies)
            {
                Console.WriteLine(movie);
            }

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}
