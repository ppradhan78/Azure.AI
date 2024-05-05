namespace MicrosoftAzure.AI.Data.BusinessObject.AISearch
{
    public interface IAzureAIDocumentSearchServicesBO
    {
        Task<List<AzureAISearchModel>> Search(string searchText);
        bool RunAndCheckIndexer();
    }
}
