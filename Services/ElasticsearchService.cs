using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Security;
using PlatformService.Configuration;

namespace PlatformService.Services
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly ElasticsearchClient _client;
        private readonly ElasticSettings _elasticSettings;
        public ElasticsearchService(IOptions<ElasticSettings> optionsMonitor)
        {
            _elasticSettings = optionsMonitor.Value;

            var settings = new ElasticsearchClientSettings(new Uri(_elasticSettings.Url));
        }
        public async Task<bool> AddOrUpdate(User user)
        {
            var response = await _client.IndexAsync(user, idx =>
            {
                idx.Index(_elasticSettings.DefaultIndex)
                .OpType(OpType.Index);
            });
            return response.IsValidResponse;
        }

        public Task<bool> AddOrUpdateBulk(IEnumerable<User> user, string indexName)
        {
            throw new NotImplementedException();
        }

        public async Task CreateIndexIfNotExistsAsync(string indexName)
        {
            if (!_client.Indices.Exists(indexName).Exists)
                await _client.Indices.CreateAsync(indexName);
        }

        public Task<User> Get(string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task<long?> RemoveAll()
        {
            throw new NotImplementedException();
        }
    }
}