using MicrosoftAzure.AI.Data.BusinessObject;
using MicrosoftAzure.AI.Data.BusinessObject.AISearch;
using MicrosoftAzure.AI.Data.Constants;
using MicrosoftAzure.AI.Data.SampleModel;

namespace MicrosoftAzure.AI.Data.Core.AISearch
{
    public class AzureAISqlServerSearchServicesCore : IAzureAISqlServerSearchServicesCore
    {
        #region Global Variable(s)
        private readonly IConfigurationSettings _configuration;
        public readonly IAzureAISqlServerSearchServicesBO _azureAISqlServerSearchServicesBO;
        #endregion

        public AzureAISqlServerSearchServicesCore(IAzureAISqlServerSearchServicesBO azureAISqlServerSearchServicesBO, IConfigurationSettings configuration)
        {
            _azureAISqlServerSearchServicesBO = azureAISqlServerSearchServicesBO;
            _configuration = configuration;
        }
        
        #region Public Method(s)
        public Task<List<AzureAISearchModel>> Search(string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                return _azureAISqlServerSearchServicesBO.Search(search);
            }
            else
            {
                throw new Exception(CommonConstants.InputSearchFailed);
            }
          
        }

        public bool RunAndCheckIndexer()
        {
            return _azureAISqlServerSearchServicesBO.RunAndCheckIndexer();
        }
        #endregion
    }
}
