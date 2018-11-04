namespace Task4.ApiClient
{
    public class LanguageEntity
    {
        public string region { get; set; }
        public string source { get; set; }
        public SourceLanguageEntity sourceLanguage { get; set; }
        public SourceLanguageEntity targetLanguage { get; set; }
        public string type { get; set; }
    }
}
