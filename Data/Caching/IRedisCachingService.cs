using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data.Caching
{
    public interface IRedisCachingService
    {
        T? GetData<T>(string key);
        void SetData<T>(string key, T data);
    }
}