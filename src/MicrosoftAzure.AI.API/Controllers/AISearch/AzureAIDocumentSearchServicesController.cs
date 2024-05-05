
namespace MicrosoftAzure.AI.API.Controllers.AISearch
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors]
    public class AzureAIDocumentSearchServicesController : ControllerBase
    {
        #region Global Variable(s)
        private readonly IConfigurationSettings _configuration;
        private readonly IAzureAIDocumentSearchServicesCore _azureAISearchServicesCore;
        private readonly ILogger _logger;
        #endregion

        public AzureAIDocumentSearchServicesController(IAzureAIDocumentSearchServicesCore azureAISearchServicesCore, IConfigurationSettings configuration, ILogger<AzureAIDocumentSearchServicesController>  logger)
        {
            _azureAISearchServicesCore = azureAISearchServicesCore;
            _configuration = configuration;
            _logger = logger;
        }
       
        #region API Method(s)
        [HttpPost]
        public async Task<IActionResult> Search(string SearchText)
        {
            try
            {
                var response = await _azureAISearchServicesCore.Search(SearchText);
                if (response.Any())
                {
                    _logger.LogError("response" + response[0].SearchContent);
                }
               
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }
        #endregion 
        
    }
}
