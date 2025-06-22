using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace PlatformService.Data.Caching
{
    public class RedisCachingService : IRedisCachingService
    {
        private readonly IDistributedCache? _cache;
        public RedisCachingService(IDistributedCache? cache)
        {
            _cache = cache;
        }
        public T? GetData<T>(string key)
        {
            var data = _cache?.GetString(key);
            if (data is null)
                return default(T);

            return JsonSerializer.Deserialize<T>(data);

        }

        public void SetData<T>(string key, T data)
        {
            var option = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            };
            _cache.SetString(key, JsonSerializer.Serialize(data), option);
        }
    }
}