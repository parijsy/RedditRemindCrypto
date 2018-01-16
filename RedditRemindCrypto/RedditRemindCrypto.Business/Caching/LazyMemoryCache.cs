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

        public TValue GetOrAdd(string key, Func<string, TValue> valueFactory, CacheItemPolicy cacheItemPolicy)
        {
            var lazyResolver = new Lazy<TValue>(() => valueFactory(key));
            var cacheResult = (Lazy<TValue>)cache.AddOrGetExisting(key, lazyResolver, cacheItemPolicy);
            return (cacheResult ?? lazyResolver).Value;
        }
    }
}
