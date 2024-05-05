using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicrosoftAzure.AI.API.Controllers.AISearch
{
    namespace ESGSurvey.API.ApiControllers.AISearch
    {
        [Route("api/[controller]")]
        [ApiController]
        [EnableCors]
        public class AzureAICosmosSearchServicesController : ControllerBase
        {
            #region Global Variable(s)
            private readonly IConfigurationSettings _configuration;
            private readonly IAzureAICosmosSearchServicesCore _azureAICosmosSearchServicesCore;
            private readonly ILogger _logger;
            #endregion

            public AzureAICosmosSearchServicesController(IAzureAICosmosSearchServicesCore azureAICosmosSearchServicesCore, IConfigurationSettings configuration, ILogger<IAzureAICosmosSearchServicesCore> logger)
            {
                _azureAICosmosSearchServicesCore = azureAICosmosSearchServicesCore;
                _configuration = configuration;
                _logger = logger;
            }

            #region API Method(s)
            [HttpPost]
            public async Task<IActionResult> Search(string SearchText)
            {
                try
                {
                    var response = await _azureAICosmosSearchServicesCore.Search(SearchText);
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

}
