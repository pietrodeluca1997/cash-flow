using CF.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace CF.Account.API.Data.MemoryDatabase
{
    public class RedisBaseRepository : IMemoryDatabaseRepository
    {
        private readonly IDistributedCache _distributedCache;

        public RedisBaseRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task AddKeyAsync(string key, string value)
        {
            await _distributedCache.SetStringAsync(key, value);
        }

        public async Task AddKeyWithTimeSpanAsync(string key, string value, TimeSpan absoluteExpirationTime, TimeSpan slidingExpirationTime)
        {
            DistributedCacheEntryOptions entryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationTime,
                SlidingExpiration = slidingExpirationTime
            };

            await _distributedCache.SetStringAsync(key, value, entryOptions);
        }

        public async Task<string> GetValueByKey(string key)
        {
            return await _distributedCache.GetStringAsync(key);
        }

        public async Task DeleteByKey(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }
    }
}
