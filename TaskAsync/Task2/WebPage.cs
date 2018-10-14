namespace Task2
{
    /// <summary>
    /// Represent a fake web page.
    /// </summary>
    public class WebPage
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }

        public override string ToString()
        {
            return $"{Title} - [{Url}]\n{Body}\n{Footer}";
        }
    }
}
