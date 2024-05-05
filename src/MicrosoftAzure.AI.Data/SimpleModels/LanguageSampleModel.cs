namespace MicrosoftAzure.AI.Data.SimpleModels
{
    public class LanguageDetails
    {
        public string Name { get; set; }
        public string NativeName { get; set; }
        public string Dir { get; set; }
    }
    public class AvailableLanguage
    {
        public Dictionary<string, LanguageDetails> Translation { get; set; }
    }
    public class AvailableLanguageDTO
    {
        public string LanguageID { get; set; }
        public string LanguageName { get; set; }
    }
    public class OcrResultDTO
    {
        public string Language { get; set; }
        public string DetectedText { get; set; }
    }
}
