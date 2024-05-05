
using MicrosoftAzure.AI.Data.BusinessObject.AISearch;
using MicrosoftAzure.AI.Data.BusinessObject.OpenAI;
using MicrosoftAzure.AI.Data.BusinessObjects.TextAnalysis;
using MicrosoftAzure.AI.Data.Core.TextAnalysis;

namespace MicrosoftAzure.API.Extension
{
    public static class DependencyRegistrationExtension
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection Services)
        {
          
            Services.AddSingleton<IConfigurationSettings, ConfigurationSettings>();
            Services.AddSingleton<ITextAnalysisCore, TextAnalysisCore>();
            Services.AddTransient<ITextAnalysisBo, TextAnalysisBo>();
            Services.AddSingleton<IAzureAIDocumentSearchServicesCore, AzureAIDocumentSearchServicesCore>();
            Services.AddTransient<IAzureAIDocumentSearchServicesBO, AzureAIDocumentSearchServicesBO>();
            Services.AddSingleton<IOpenAIServiceCore, OpenAIServiceCore>();
            Services.AddTransient<IOpenAIServiceBO, OpenAIServiceBO>();
            
            return Services;
        }
        public static IServiceCollection AddApiDependencies(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            //Services.Configure<ConfigurationSettings>(builder.Configuration.GetSection("AzureAISearch"));
            Services.AddSwaggerGen();
            Services.AddControllers();
            Services.AddApplicationInsightsTelemetry();
            Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            return Services;
        }
    }
}
