using MicrosoftAzure.AI.Data.SampleModel;

namespace MicrosoftAzure.AI.Data.Core.AISearch
{
    public interface IAzureAICosmosSearchServicesCore
    {
        Task<List< AzureAISearchModel>> Search(string search);
        bool RunAndCheckIndexer();
    }
}
