using System;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            var foo = new Foo
            {
                Id = 1,
                Name = "FakeName",
                Age = 30
            };

            var mappingGenerator = new MappingGenerator();
            var mapper = mappingGenerator.GenerateMapper<Foo, Bar>();

            var result = mapper.Map(foo);

            Console.WriteLine($"The result of mapping:\n{result}");

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey();
        }
    }
}
