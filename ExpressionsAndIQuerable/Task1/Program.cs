using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Task1
{
    class Program
    {
        private const string FakeCallerName = "FakeCallerName";

        static void Main(string[] args)
        {
            Expression<Func<int, int>> initialExpression = (number) => number * 2 + (number + 1) + (5 + 3) + (1 + number);
            var addToIncrementTransformer = new AddToIncrementTransformer();

            var transformedExpression = addToIncrementTransformer.VisitAndConvert(initialExpression, FakeCallerName);

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}
