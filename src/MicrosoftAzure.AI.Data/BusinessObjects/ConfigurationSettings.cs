
namespace MicrosoftAzure.AI.Data.BusinessObject
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        #region Global Variable(s)
        private readonly IConfiguration _configuration;
        #endregion

        public ConfigurationSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region   Search
        public string SearchServiceName => _configuration["AIServices:Search:SearchServiceName"];

        public string SearchIndexName => _configuration["AIServices:Search:SearchIndexName"];

        public string SearchApiKey => _configuration["AIServices:Search:SearchApiKey"];

        public string SearchServiceUrl => _configuration["AIServices:Search:SearchServiceUrl"];

        public string SearchIndexerName => _configuration["AIServices:Search:SearchIndexerName"];
        #endregion

        #region AzureStorage
        public string StorageConnectionString => _configuration["AzureStorage:StorageConnectionString"];

        public string StorageBlobContainerName => _configuration["AzureStorage:StorageBlobContainerName"];
        #endregion

        #region OpenAPI
        public string OpenAIApiBase => _configuration["AzureOpenAI:OpenAIApiBase"];

        public string OpenAIKey => _configuration["AzureOpenAI:OpenAIKey"];

        public string OpenAIDeploymentId => _configuration["AzureOpenAI:OpenAIDeploymentId"];

        #endregion

        #region TextAnalytics
        public string TextAnalyticsServiceName => _configuration["AIServices:TextAnalytics:TextAnalyticsServiceName"];

        public string TextAnalyticsServiceApiKey => _configuration["AIServices:TextAnalytics:TextAnalyticsServiceApiKey"];

        public string TextAnalyticsServiceUrl => _configuration["AIServices:TextAnalytics:TextAnalyticsServiceUrl"];
        #endregion TextAnalytics
        #region Cosmos
        public string CosmosDBAccount => _configuration["CosmosDB:CosmosDBAccount"];
        public string CosmosDBAccountUri => _configuration["CosmosDB:CosmosDBAccountUri"];
        public string CosmosDBAccountPrimaryKey => _configuration["CosmosDB:CosmosDBAccountPrimaryKey"];
        public string CosmosDbName => _configuration["CosmosDB:CosmosDbName"];
        public string ConnectionStringSetting => _configuration["CosmosDB:ConnectionStringSetting"];
        #endregion
    }
}
