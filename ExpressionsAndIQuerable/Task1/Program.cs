using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Task1
{
    class Program
    {
        private const string FakeCallerName = "FakeCallerName";

        static void Main(string[] args)
        {
            int param = 10;
            Expression<Func<int, int>> initialExpression = (number) => number * 2 + (number + 1) + (5 + 3) + (1 + number);
            var addToIncrementTransformer = new AddToIncrementTransformer();
            var transformedExpression = addToIncrementTransformer.VisitAndConvert(initialExpression, FakeCallerName);

            Console.WriteLine($"Replace x + 1 by increment.\nInitial expression: {initialExpression}\nTransformed expression: {transformedExpression}\nInvoke with number = {param}. Result = {transformedExpression.Compile().Invoke(param)}\n\n");

            var dictionary = new Dictionary<string, int>
            {
                {"x", 100},
                {"y", 200},
                {"z", 300}
            };

            int xParam = 1;
            int yParam = 2;
            int zParam = 3;

            Expression<Func<int, int, int, double>> rootMeanSquareExpression = (x, y, z) => Math.Sqrt((x * x + y * y + z * z) / 3);
            var parameterTransformer = new ParameterToConstantTransformer<int>(dictionary);
            var transformed = parameterTransformer.VisitAndConvert(rootMeanSquareExpression, FakeCallerName);

            Console.WriteLine($"Replace parameter expression by constant expression.\nInitial expression: {rootMeanSquareExpression}\nTransformed expression: {transformed}\nInvoke with the following parameters x = {xParam}, y = {yParam}, z = {zParam}. Result = {transformed.Compile()(xParam, yParam, zParam)}");

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}
