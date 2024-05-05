
namespace MicrosoftAzure.AI.Data.Core.OpenAI
{
    public class OpenAIServiceCore : IOpenAIServiceCore
    {
        #region Global Variable(s)
        private readonly IConfigurationSettings _configuration;
        public readonly IOpenAIServiceBO _openAIServiceBO;
        #endregion

        public OpenAIServiceCore(IOpenAIServiceBO openAIServiceBO, IConfigurationSettings configuration)
        {
            _openAIServiceBO = openAIServiceBO;
            _configuration = configuration;
        }

        #region Public Method(s)
        public Task<OpenAIOutputResponseModel> GenerateChatTextAsync(string searchText)
        //public Task<Response<ChatCompletions>> GenerateChatTextAsync(string searchText)
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                return _openAIServiceBO.GenerateChatTextAsync(searchText);
            }
            else
            {
                throw new Exception(CommonConstants.InputSearchFailed);
            }
           
        }
        #endregion
    }
}
