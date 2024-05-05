
namespace MicrosoftAzure.AI.Data.BusinessObject.OpenAI
{
    public class OpenAIServiceBO : IOpenAIServiceBO
    {
        #region Global Variable(s)
        private readonly IConfigurationSettings _configuration;
        private readonly OpenAIClient _client;
        private ChatCompletionsOptions _options;
        #endregion

        public OpenAIServiceBO(IConfigurationSettings configuration)
        {
            this._configuration = configuration;
            _client = new OpenAIClient(new Uri(_configuration.OpenAIApiBase), new AzureKeyCredential(_configuration.OpenAIKey));
            ConnectToAzureAISearch();
        }

        #region Public Method(s)
        //public async Task<Response<ChatCompletions>> GenerateChatTextAsync(string searchText);
        public async Task<OpenAIOutputResponseModel> GenerateChatTextAsync(string searchText)
        {
            List<ChatMessage> messages = new List<ChatMessage>()
            {
                new ChatMessage(ChatRole.User, searchText)
            };
            InitializeChatMessages(messages);
            var result = await _client.GetChatCompletionsAsync(_configuration.OpenAIDeploymentId, _options);
            if (result != null && result.Value != null && result.Value.Choices.Any())
            {
                string rawContent = string.Empty;
                if (result.GetRawResponse() != null && result.GetRawResponse().Content != null)
                {
                    rawContent = result.GetRawResponse().Content.ToString();
                }
                return new OpenAIOutputResponseModel { RawResponse = result, Content = result.Value.Choices[0].Message.Content, RawContent = rawContent };
            }
            else
            {
                return new OpenAIOutputResponseModel { RawResponse = null, Content = CommonConstants.SearchResultNotFound, RawContent = "" };
            }
        }
        #endregion

        #region Private Method(s)
        private void ConnectToAzureAISearch()
        {
            _options = new ChatCompletionsOptions()
            {
                AzureExtensionsOptions = new AzureChatExtensionsOptions()
                {
                    Extensions =
                    {
                        new AzureCognitiveSearchChatExtensionConfiguration()
                        {
                            SearchEndpoint = new Uri(_configuration.SearchServiceUrl),
                            IndexName = _configuration.SearchIndexName,
                            SearchKey = new AzureKeyCredential(_configuration.SearchApiKey)
                        }
                    }
                }
            };
        }

        private void InitializeChatMessages(List<ChatMessage> chatMessages)
        {
            foreach (var chatMessage in chatMessages)
            {
                _options.Messages.Add(chatMessage);
            }
        }
        #endregion
    }
}
