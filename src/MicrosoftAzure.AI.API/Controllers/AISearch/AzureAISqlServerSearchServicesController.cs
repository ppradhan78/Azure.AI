namespace MicrosoftAzure.AI.API.Controllers.AISearch
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class AzureAISqlServerSearchServicesController : ControllerBase
    {
        #region Global Variable(s)
        private readonly IConfigurationSettings _configuration;
        private readonly IAzureAISqlServerSearchServicesCore _azureAISqlServerSearchServicesCore;
        private readonly ILogger _logger;
        #endregion

        public AzureAISqlServerSearchServicesController(IAzureAISqlServerSearchServicesCore azureAISqlServerSearchServicesCore, IConfigurationSettings configuration, ILogger<AzureAISqlServerSearchServicesController> logger)
        {
            _azureAISqlServerSearchServicesCore = azureAISqlServerSearchServicesCore;
            _configuration = configuration;
            _logger = logger;
        }

        #region API Method(s)
        [HttpPost]
        public async Task<IActionResult> Search(string SearchText)
        {
            try
            {
                var response = await _azureAISqlServerSearchServicesCore.Search(SearchText);
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

