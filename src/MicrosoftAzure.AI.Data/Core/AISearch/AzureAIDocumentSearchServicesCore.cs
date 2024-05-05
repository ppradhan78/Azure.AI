using MicrosoftAzure.AI.Data.BusinessObject;
using MicrosoftAzure.AI.Data.BusinessObject.AISearch;
using MicrosoftAzure.AI.Data.Constants;
using MicrosoftAzure.AI.Data.SampleModel;

namespace MicrosoftAzure.AI.Data.Core.AISearch
{
    public class AzureAIDocumentSearchServicesCore : IAzureAIDocumentSearchServicesCore
    {
        #region Global Variable(s)
        private readonly IConfigurationSettings _configuration;
        public readonly IAzureAIDocumentSearchServicesBO _AzureAISearchServicesBO;
        #endregion

        public AzureAIDocumentSearchServicesCore(IAzureAIDocumentSearchServicesBO azureAISearchServicesBO, IConfigurationSettings configuration)
        {
            _AzureAISearchServicesBO = azureAISearchServicesBO;
            _configuration = configuration;
        }
        
        #region Public Method(s)
        public Task<List<AzureAISearchModel>> Search(string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                return _AzureAISearchServicesBO.Search(search);
            }
            else
            {
                throw new Exception(CommonConstants.InputSearchFailed);
            }
          
        }

        public bool RunAndCheckIndexer()
        {
            return _AzureAISearchServicesBO.RunAndCheckIndexer();
        }
        #endregion
    }
}
