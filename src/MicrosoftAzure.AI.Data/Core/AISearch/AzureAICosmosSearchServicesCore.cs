using MicrosoftAzure.AI.Data.BusinessObject;
using MicrosoftAzure.AI.Data.BusinessObject.AISearch;
using MicrosoftAzure.AI.Data.Constants;
using MicrosoftAzure.AI.Data.SampleModel;

namespace MicrosoftAzure.AI.Data.Core.AISearch
{
    public class AzureAICosmosSearchServicesCore : IAzureAICosmosSearchServicesCore
    {
        #region Global Variable(s)
        private readonly IConfigurationSettings _configuration;
        public readonly IAzureAICosmosSearchServicesBO _azureAICosmosSearchServicesBO;
        #endregion

        public AzureAICosmosSearchServicesCore(IAzureAICosmosSearchServicesBO azureAICosmosSearchServicesBO, IConfigurationSettings configuration)
        {
            _azureAICosmosSearchServicesBO = azureAICosmosSearchServicesBO;
            _configuration = configuration;
        }
        
        #region Public Method(s)
        public Task<List<AzureAISearchModel>> Search(string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                return _azureAICosmosSearchServicesBO.Search(search);
            }
            else
            {
                throw new Exception(CommonConstants.InputSearchFailed);
            }
          
        }

        public bool RunAndCheckIndexer()
        {
            return _azureAICosmosSearchServicesBO.RunAndCheckIndexer();
        }
        #endregion
    }
}
