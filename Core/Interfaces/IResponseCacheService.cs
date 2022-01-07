using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKay, object response, TimeSpan timeToLive); // Use this to cache response.
        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}