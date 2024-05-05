namespace MicrosoftAzure.AI.Data.BusinessObject.AISearch
{
    public interface IAzureAISqlServerSearchServicesBO
    {
        Task<List<AzureAISearchModel>> Search(string searchText);
        bool RunAndCheckIndexer();
    }
}
