using MicrosoftAzure.AI.Data.BusinessObjects.TextAnalysis;
using MicrosoftAzure.AI.Data.SimpleModels;

namespace MicrosoftAzure.AI.Data.Core.TextAnalysis
{
    public class TextAnalysisCore : ITextAnalysisCore
    {
        private readonly ITextAnalysisBo _textAnalysisBo;
        public TextAnalysisCore(ITextAnalysisBo textAnalysisBo)
        {
            _textAnalysisBo = textAnalysisBo;
        }

        public TextAnalysisSampleModel AnalyzeText(string contents)
        {
           return _textAnalysisBo.AnalyzeText(contents);
        }
    }
}
