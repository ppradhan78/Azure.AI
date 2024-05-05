namespace MicrosoftAzure.AI.Data.BusinessObject
{
    public interface IConfigurationSettings
    {
        #region Search
        string SearchServiceName { get; }
        string SearchApiKey { get; }
        string SearchServiceUrl { get; }
        #endregion

        #region AzureStorage
        string StorageConnectionString { get; }
        string StorageBlobContainerName { get; }
        #endregion


        string OpenAIApiBase { get; }
        string OpenAIKey { get; }
        string OpenAIDeploymentId { get; }

        #region TextAnalytics
        string TextAnalyticsServiceName { get; }
        string TextAnalyticsServiceApiKey { get; }
        string TextAnalyticsServiceUrl { get; }
        #endregion

        #region Cosmos
        string CosmosDBAccountUri { get; }
        string CosmosDBAccountPrimaryKey { get; }
        string CosmosDbName { get; }
        string CosmosDBAccount { get; }
        #endregion


    }
}
