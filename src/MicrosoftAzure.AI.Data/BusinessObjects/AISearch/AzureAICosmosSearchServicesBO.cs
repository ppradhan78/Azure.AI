



namespace MicrosoftAzure.AI.Data.BusinessObject.AISearch
{
    public class AzureAICosmosSearchServicesBO : IAzureAICosmosSearchServicesBO
    {
        #region Global Variable(s)

        private readonly IConfigurationSettings _configuration;
        private const string SearchIndexName = "cosmosdb-index";
        private const string SearchIndexerName = "cosmosdb-indexer";
        #endregion

        public AzureAICosmosSearchServicesBO(IConfigurationSettings configuration)
        {
            _configuration = configuration;
        }



        #region Public Method(s)

        public async Task<List<AzureAICategorySearchModel>> Search(string searchText)
        {
            try
            {
                SearchServiceClient serviceClient = new SearchServiceClient(_configuration.SearchServiceName, new SearchCredentials(_configuration.SearchApiKey));
                ISearchIndexClient indexClient = serviceClient.Indexes.GetClient(SearchIndexName);
                SearchParameters parameters = new SearchParameters();
                parameters.HighlightFields = new List<string> { "Description" };
                parameters.HighlightPreTag = "<br/>";
                parameters.HighlightPostTag = "<br/>";
                var result = indexClient.Documents.SearchAsync(searchText, parameters).Result;
                List<AzureAICategorySearchModel> searchResult = new List<AzureAICategorySearchModel>();

                foreach (var item in result.Results)
                {

                    var searchModel = new AzureAICategorySearchModel();
                    if (item.Document.ContainsKey("CategoryID"))
                    {
                        if (item.Document["CategoryID"] != null)
                        {
                            searchModel.CategoryID = item.Document["CategoryID"].ToString();
                        }
                    }
                    if (item.Document.ContainsKey("CategoryName"))
                    {
                        if (item.Document["CategoryName"] != null)
                        {
                            searchModel.CategoryName = item.Document["CategoryName"].ToString();
                        }
                    }
                    if (item.Document.ContainsKey("Description"))
                    {
                        if (item.Document["Description"] != null)
                        {
                            searchModel.Description = item.Document["Description"].ToString();
                        }
                    }
                    if (item.Document.ContainsKey("people"))
                    {
                        searchModel.People = string.Join(",", item.Document["people"]);
                    }
                    if (item.Document.ContainsKey("keyphrases"))
                    {
                        searchModel.Keyphrases = string.Join(",", item.Document["keyphrases"]);
                       
                    }
                    if (item.Highlights != null)
                    {
                        foreach (var data in item.Highlights["Description"].ToList())
                        {
                            searchModel.HighlightedText += data;
                        }
                    }
                    searchResult.Add(searchModel);
                }

                return searchResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool RunAndCheckIndexer()
        {
            try
            {
                SearchIndexerClient indexerClient = new SearchIndexerClient(
                                    new Uri(_configuration.SearchServiceUrl),
                                    new AzureKeyCredential(_configuration.SearchApiKey));

                Response indexerResponse = indexerClient.RunIndexer(SearchIndexerName);
                if (indexerResponse != null)
                {
                    if (indexerResponse.Status == 202)
                    {
                        Thread.Sleep(50000);

                        if (CheckIndexerStatus(indexerClient, SearchIndexerName))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        private bool CheckIndexerStatus(SearchIndexerClient searchIndexerClient, string indexerName)
        {
            bool isIndexerSuccess = false;
            SearchIndexerStatus execInfo = searchIndexerClient.GetIndexerStatus(indexerName);
            Azure.Search.Documents.Indexes.Models.IndexerExecutionResult lastResult = execInfo.LastResult;

            if (lastResult.ErrorMessage != null)
            {
                isIndexerSuccess = false;
            }
            else
            {
                isIndexerSuccess = true;
            }
            return isIndexerSuccess;
        }

    }
}
