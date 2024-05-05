using MicrosoftAzure.AI.Data.SimpleModels;

namespace MicrosoftAzure.AI.Data.BusinessObjects.TextAnalysis
{
    public interface ITextAnalysisBo
    {
        TextAnalysisSampleModel AnalyzeText(string contents);
    }
}
