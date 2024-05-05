



namespace MicrosoftAzure.AI.Data.BusinessObject.AISearch
{
    public class AzureAIDocumentSearchServicesBO : IAzureAIDocumentSearchServicesBO
    {
        #region Global Variable(s)

        private readonly IConfigurationSettings _configuration;
        #endregion

        public AzureAIDocumentSearchServicesBO(IConfigurationSettings configuration)
        {
            _configuration = configuration;
        }



        #region Public Method(s)

        public async Task<List<AzureAISearchModel>> Search(string searchText)
        {
            try
            {
                SearchServiceClient serviceClient = new SearchServiceClient(_configuration.SearchServiceName, new SearchCredentials(_configuration.SearchApiKey));
                ISearchIndexClient indexClient = serviceClient.Indexes.GetClient(_configuration.SearchIndexName);
                SearchParameters parameters = new SearchParameters();
                parameters.HighlightFields = new List<string> { "content" };
                parameters.HighlightPreTag = "<br/>";
                parameters.HighlightPostTag = "<br/>";
                var result = indexClient.Documents.SearchAsync(searchText, parameters).Result;
                List<AzureAISearchModel> searchResult = new List<AzureAISearchModel>();

                foreach (var item in result.Results)
                {

                    var searchModel = new AzureAISearchModel();
                    if (item.Document.ContainsKey("metadata_storage_path"))
                    {
                        if (item.Document["metadata_storage_path"] != null)
                        {
                            string? path = item.Document["metadata_storage_path"].ToString();
                            path = path?.Substring(0, path.Length - 1);
                            var bitData = WebEncoders.Base64UrlDecode(path);
                            searchModel.FilePath = System.Text.ASCIIEncoding.ASCII.GetString(bitData);
                        }
                    }
                    if (item.Document.ContainsKey("content"))
                    {
                        if (item.Document["content"] != null)
                        {
                            searchModel.SearchContent = item.Document["content"].ToString();
                        }
                    }
                    if (item.Document.ContainsKey("metadata_storage_content_type"))
                    {
                        if (item.Document["metadata_storage_content_type"] != null)
                        {
                            searchModel.ContentType = item.Document["metadata_storage_content_type"].ToString();
                        }
                    }
                    if (item.Document.ContainsKey("metadata_storage_size"))
                    {
                        if (item.Document["metadata_storage_size"] != null)
                        {
                            searchModel.Size = item.Document["metadata_storage_size"].ToString();
                        }
                    }
                    if (item.Document.ContainsKey("metadata_storage_last_modified"))
                    {
                        if (item.Document["metadata_storage_last_modified"] != null)
                        {
                            searchModel.ModifiedDate = item.Document["metadata_storage_last_modified"].ToString();
                        }
                    }

                    if (item.Document.ContainsKey("metadata_creation_date"))
                    {
                        if (item.Document["metadata_creation_date"] != null)
                        {
                            searchModel.CreationDate = item.Document["metadata_creation_date"].ToString();
                        }
                    }
                    if (item.Document.ContainsKey("metadata_storage_name"))
                    {
                        if (item.Document["metadata_storage_name"] != null)
                        {
                            searchModel.FileName = item.Document["metadata_storage_name"].ToString();
                        }
                    }
                    if (item.Document.ContainsKey("metadata_storage_file_extension"))
                    {
                        if (item.Document["metadata_storage_file_extension"] != null)
                        {
                            searchModel.Extension = item.Document["metadata_storage_file_extension"].ToString();
                        }
                    }
                    if (item.Document.ContainsKey("metadata_language"))
                    {
                        if (item.Document["metadata_language"] != null)
                        {
                            searchModel.Extension = item.Document["metadata_language"].ToString();
                        }
                    }
                    if (item.Document.ContainsKey("merged_content"))
                    {
                        if (item.Document["merged_content"] != null)
                        {
                            searchModel.Extension = item.Document["merged_content"].ToString();
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
                        foreach (var data in item.Highlights["content"].ToList())
                        {
                            searchModel.HighlightedText += data;
                        }
                    }
                    searchModel.Score = item.Score.ToString();
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

                Response indexerResponse = indexerClient.RunIndexer(_configuration.SearchIndexerName);
                if (indexerResponse != null)
                {
                    if (indexerResponse.Status == 202)
                    {
                        Thread.Sleep(50000);

                        if (CheckIndexerStatus(indexerClient, _configuration.SearchIndexerName))
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
