using System;
using QueryableProviderForMovieDb;

namespace ConsoleApplicationForQueryableProviderForMovieDb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nEnter you query to start search movies:");
            string query = Console.ReadLine();

            var movieDbClient = new MovieDbClient();
            var discoverResult = movieDbClient.SearchMovies(query);

            Console.WriteLine($"\nSearch results:\n{discoverResult}");

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}
