namespace Task2
{
    public class Bar
    {
        public Bar() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"The Bar class. Id: {Id}; Name: {Name}; Age: {Age};";
        }
    }
}
