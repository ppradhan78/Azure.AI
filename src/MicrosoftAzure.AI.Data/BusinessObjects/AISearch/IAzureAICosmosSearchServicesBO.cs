namespace MicrosoftAzure.AI.Data.BusinessObject.AISearch
{
    public interface IAzureAICosmosSearchServicesBO
    {
        Task<List<AzureAICategorySearchModel>> Search(string searchText);
        bool RunAndCheckIndexer();
    }
}
