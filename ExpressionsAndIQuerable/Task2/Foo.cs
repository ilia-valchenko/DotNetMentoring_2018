namespace Task2
{
    public class Foo
    {
        public Foo() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"The Foo class. Id: {Id}; Name: {Name}; Age: {Age};";
        }
    }
}
