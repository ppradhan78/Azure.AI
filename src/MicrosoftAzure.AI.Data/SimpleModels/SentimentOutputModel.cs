namespace MicrosoftAzure.AI.Data.SimpleModels
{
    public class TextAnalysisSampleModel
    {
        public SentimentOutputModel SentimentOutputModel { get; set; }
        public LanguageModel LanguageDetails { get; set; }
        
        public List<KeyPhraseCollectionModel> keyPhrases { get; set; }
        public List<RecognizeEntitieModel> RecognizeEntities { get; set; }
        public PiiEntityCollectionModel PiiEntityCollection { get; set; }
        public LinkedEntityCollectionModel LinkedEntityCollection { get; set; }
    }
    /// <summary>
    /// Analyze Sentiment
    /// Determine the positive, negative, neutral or mixed sentiment 
    /// </summary>
    public class SentimentOutputModel
    {
        public double Positive { get; set; }
        public double Neutral { get; set; }
        public double Negative { get; set; }
    }
    /// <summary>
    /// Language detection.
    /// determine the language
    /// </summary>
    public class LanguageModel
    {
        public string Name { get; set; }
        public double ConfidenceScore { get; set; }
    }
    /// <summary>
    /// Extract Key Phrases
    /// Keyphrase or keyword extraction
    /// extracts important words and phrases from the input text. 
    /// identifies the main points in a text document
    /// </summary>
    public class KeyPhraseCollectionModel
    {
        public string keyPhrase { get; set; }
    }

    /// <summary>
    /// entities into categories such as person, location, or organization.
    /// </summary>
    public class RecognizeEntitieModel
    {
        public string Text { get; set; }
        public int Offset { get; set; }
        public string SubCategory { get; set; }
        public int Length { get; set; }
        public double ConfidenceScore { get; set; }
    }
    /// <summary>
    /// Personally Identifiable Information
    /// such as US social security number, drivers license number, or credit card number.
    /// </summary>
    public class PiiEntityCollectionModel
    {
        public string RedactedText { get; set; }
        public int Count { get; set; }
        public List<PiiEntityModel> PiiEntitys { get; set; }
    }
    public class PiiEntityModel
    {
        public string Text { get; set; }
        public string SubCategory { get; set; }
        public double ConfidenceScore { get; set; }

    }
    /// <summary>
    /// include information linking the entities 
    /// </summary>
    public class LinkedEntityCollectionModel
    {
        public List<LinkedEntityModel> LinkedEntitys { get; set; }
    }
    public class LinkedEntityModel
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string DataSource { get; set; }
        public string DataSourceEntityId { get; set; }
        public Uri Url { get; set; }
        public List<LinkedEntityMatchModel> Matches { get; set; }
    }
    public class LinkedEntityMatchModel
    {
        public string Text { get; set; }
        public int Offset { get; set; }
        public double ConfidenceScore { get; set; }
        public int Length { get; set; }
    }
}
