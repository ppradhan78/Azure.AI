namespace MicrosoftAzure.AI.Data.BusinessObject.AISearch
{
    public interface IAzureAICosmosSearchServicesBO
    {
        Task<List<AzureAISearchModel>> Search(string searchText);
        bool RunAndCheckIndexer();
    }
}
