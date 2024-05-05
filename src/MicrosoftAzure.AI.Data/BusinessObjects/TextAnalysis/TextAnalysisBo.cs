using Azure.AI.TextAnalytics;
using Azure.Identity;
using MicrosoftAzure.AI.Data.SimpleModels;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace MicrosoftAzure.AI.Data.BusinessObjects.TextAnalysis
{
    public class TextAnalysisBo : ITextAnalysisBo
    {
        #region Global Variable(s)

        private readonly IConfigurationSettings _configuration;
        #endregion
        public TextAnalysisBo(IConfigurationSettings configuration)
        {
            _configuration = configuration;
        }
        public TextAnalysisSampleModel AnalyzeText(string contents)
        {
            try
            {
                var output = new TextAnalysisSampleModel();
                var client = GetTextAnalyticsClient();
                Response<DocumentSentiment> response = client.AnalyzeSentiment(contents);
                DocumentSentiment docSentiment = response.Value;
                if (docSentiment != null)
                {
                    output.SentimentOutputModel = new SentimentOutputModel
                    {
                        Positive = docSentiment.ConfidenceScores.Positive,
                        Negative = docSentiment.ConfidenceScores.Negative,
                        Neutral = docSentiment.ConfidenceScores.Neutral
                    };

                }
                Response<DetectedLanguage> detectedLanguageresponse = client.DetectLanguage(contents);
                if (detectedLanguageresponse != null)
                {
                    DetectedLanguage detectedLanguage = detectedLanguageresponse.Value;
                    output.LanguageDetails = new LanguageModel { Name = detectedLanguage.Name, ConfidenceScore = detectedLanguage.ConfidenceScore };
                }
                Response<KeyPhraseCollection> keyPhraseCollectionResponce = client.ExtractKeyPhrases(contents);
                if (keyPhraseCollectionResponce != null)
                {
                    KeyPhraseCollection keyPhrases = keyPhraseCollectionResponce.Value;
                    var keyPhraseList = new List<KeyPhraseCollectionModel>();
                    foreach (string keyPhrase in keyPhrases)
                    {
                        keyPhraseList.Add(new KeyPhraseCollectionModel { keyPhrase = keyPhrase });
                    }
                    output.keyPhrases = keyPhraseList;
                }
                Response<CategorizedEntityCollection> responseEntity = client.RecognizeEntities(contents);
                CategorizedEntityCollection entitiesInDocument = responseEntity.Value;
                var RecognizeEntities = new List<RecognizeEntitieModel>();
                foreach (CategorizedEntity entity in entitiesInDocument)
                {
                    RecognizeEntities.Add(new RecognizeEntitieModel
                    {
                        Text = entity.Text,
                        Offset = entity.Offset,
                        Length = entity.Length,
                        SubCategory = entity.SubCategory,
                        ConfidenceScore = entity.ConfidenceScore
                    });

                }
                output.RecognizeEntities = RecognizeEntities;
                Response<PiiEntityCollection> responsePiiEntity = client.RecognizePiiEntities(contents);
                PiiEntityCollection entities = responsePiiEntity.Value;
                output.PiiEntityCollection = new PiiEntityCollectionModel
                {
                    RedactedText = entities.RedactedText,
                    Count = entities.Count,

                };
                var PiiEntitys = new List<PiiEntityModel>();
                foreach (PiiEntity entity in entities)
                {
                    PiiEntitys.Add(
                        new PiiEntityModel { Text = entity.Text, ConfidenceScore = entity.ConfidenceScore, SubCategory = entity.SubCategory }
                        );
                }
                output.PiiEntityCollection.PiiEntitys = PiiEntitys;

                Response<LinkedEntityCollection> responseLinkedEntity = client.RecognizeLinkedEntities(contents);
                LinkedEntityCollection linkedEntities = responseLinkedEntity.Value;

                var linkedEntityCollectionModel = new LinkedEntityCollectionModel();
                var linkedEntityModel = new LinkedEntityModel();
                var linkedEntityMatchModel = new LinkedEntityMatchModel();
                var matchList = new List<LinkedEntityMatchModel>();
                var LinkedEntitys =new List<LinkedEntityModel>();
                foreach (LinkedEntity linkedEntity in linkedEntities)
                {
                    linkedEntityModel.Name = linkedEntity.Name;
                    linkedEntityModel.Language = linkedEntity.Language;
                    linkedEntityModel.DataSource = linkedEntity.DataSource;
                    linkedEntityModel.Url = linkedEntity.Url;
                    linkedEntityModel.DataSourceEntityId = linkedEntity.DataSourceEntityId;
                    foreach (LinkedEntityMatch match in linkedEntity.Matches)
                    {
                        linkedEntityMatchModel.Offset = match.Offset;
                        linkedEntityMatchModel.Text = match.Text;
                        linkedEntityMatchModel.ConfidenceScore = match.ConfidenceScore;
                        linkedEntityMatchModel.Length = match.Length;
                        matchList.Add(linkedEntityMatchModel);
                    }
                    linkedEntityModel.Matches = matchList;
                    LinkedEntitys.Add(linkedEntityModel);
                   
                }
                linkedEntityCollectionModel.LinkedEntitys = LinkedEntitys;
                output.LinkedEntityCollection = linkedEntityCollectionModel;
                return output;
            }
            catch (Exception ex)
            {
                //https://learn.microsoft.com/en-us/dotnet/api/overview/azure/ai.textanalytics-readme?view=azure-dotnet
                throw;
            }
        }

        private TextAnalyticsClient GetTextAnalyticsClient()
        {
            //https://learn.acloud.guru/handson/7a20c205-b2c6-47ce-ac63-f276f766c9d7
            Uri endpoint = new Uri(_configuration.TextAnalyticsServiceUrl);
            AzureKeyCredential credential = new AzureKeyCredential(_configuration.TextAnalyticsServiceApiKey);
            TextAnalyticsClient client = new TextAnalyticsClient(endpoint, credential);

            return client;
        }
    }
}
