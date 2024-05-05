using MicrosoftAzure.AI.Data.SimpleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftAzure.AI.Data.Core.TextAnalysis
{
    public interface ITextAnalysisCore
    {
        TextAnalysisSampleModel AnalyzeText(string contents);

    }
}
