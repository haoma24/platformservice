using Elastic.Clients.Elasticsearch.Security;

namespace PlatformService.Services
{
    public interface IElasticsearchService
    {
        // create index
        Task CreateIndexIfNotExistsAsync(string indexName);

        // add or update user
        Task<bool> AddOrUpdate(User user);

        // add or update user bulk
        Task<bool> AddOrUpdateBulk(IEnumerable<User> user, string indexName);

        // get user
        Task<User> Get(string key);

        // get all users
        Task<List<User>> GetAll();

        // remove user
        Task<bool> Remove(string key);

        // remove all
        Task<long?> RemoveAll();
    }
}