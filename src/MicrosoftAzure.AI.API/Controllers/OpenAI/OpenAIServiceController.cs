
namespace ESGSurvey.API.ApiControllers.OpenAI
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIServiceController : ControllerBase
    {
        #region Global Variable(s)
        private readonly IConfigurationSettings _configuration;
        private readonly IOpenAIServiceCore _openAIServiceCore;
        private readonly ILogger _logger;
        #endregion

        public OpenAIServiceController(IOpenAIServiceCore openAIServiceCore, IConfigurationSettings configuration, ILogger<OpenAIServiceController> logger)
        {
            _openAIServiceCore = openAIServiceCore;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost(""), DisableRequestSizeLimit]
        public async Task<IActionResult> Search(string SearchText)
        {
            try
            {
                var output = await _openAIServiceCore.GenerateChatTextAsync(SearchText);
                return Ok(output);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

    }
}
