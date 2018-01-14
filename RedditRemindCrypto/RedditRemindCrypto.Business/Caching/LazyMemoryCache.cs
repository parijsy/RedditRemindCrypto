using System;
using System.Runtime.Caching;

namespace RedditRemindCrypto.Business.Caching
{
    public class LazyMemoryCache<TValue>
    {
        private readonly MemoryCache cache;

        public LazyMemoryCache(string name)
        {
            cache = new MemoryCache(name);
        }

        public TValue GetOrAdd(string key, Func<string, TValue> valueFactory)
        {
            return GetOrAdd(key, valueFactory, DefaultCacheItemPolicy());
        }

        public TValue GetOrAdd(string key, Func<string, TValue> valueFactory, CacheItemPolicy cacheItemPolicy)
        {
            var lazyResolver = new Lazy<TValue>(() => valueFactory(key));
            var cacheResult = (Lazy<TValue>)cache.AddOrGetExisting(key, lazyResolver, cacheItemPolicy);
            return (cacheResult ?? lazyResolver).Value;
        }

        private CacheItemPolicy DefaultCacheItemPolicy()
        {
            return new CacheItemPolicy
            {
                SlidingExpiration = new TimeSpan(hours: 4, minutes: 0, seconds: 0)
            };
        }
    }
}
