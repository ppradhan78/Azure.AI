using Microsoft.AspNetCore.Mvc;
using MicrosoftAzure.AI.API.Utlity;
using MicrosoftAzure.AI.Data.Core.TextAnalysis;


namespace MicrosoftAzure.AI.API.Controllers.TextAnalysis
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextAnalysisApiController : ControllerBase
    {
        ITextAnalysisCore _textAnalysisCore;
        private readonly IConfigurationSettings _configuration;
        private readonly ILogger _logger;

        public TextAnalysisApiController(ITextAnalysisCore textAnalysisCore,IConfigurationSettings configuration, ILogger<TextAnalysisApiController> logger)
        {
            _textAnalysisCore = textAnalysisCore;
            _configuration = configuration;
            _logger = logger;
        }
       

        [HttpPost]
        public IActionResult AnalyzeText(IFormFile file)
        {
            var result = new StringBuilder();
            try
            {
                if (file.Length>0 && FileHelper.IsFileExtensionAllowed(file.FileName))
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                            result.AppendLine(reader.ReadLine());
                    }
                    return Ok( _textAnalysisCore.AnalyzeText(result.ToString()));
                }
                else
                {
                    return BadRequest();
                }
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        
    }
}
