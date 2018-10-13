using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfrastructureLibrary
{
    public static class Infrastructure
    {

        public static void PrintElementsOfCollection<T>(ICollection<T> collection)
        {
            if (collection == null || !collection.Any())
            {
                throw new ArgumentException("The collection is null or empty.");
            }

            var sb = new StringBuilder();

            foreach (var item in collection)
            {
                sb.Append($"{item} ");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
