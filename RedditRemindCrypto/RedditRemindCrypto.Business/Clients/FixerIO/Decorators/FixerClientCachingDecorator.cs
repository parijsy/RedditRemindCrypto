using RedditRemindCrypto.Business.Caching;
using RedditRemindCrypto.Business.Clients.FixerIO.Models;
using System;
using System.Runtime.Caching;

namespace RedditRemindCrypto.Business.Clients.FixerIO.Decorators
{
    public class FixerClientCachingDecorator : IFixerClient
    {
        private static readonly LazyMemoryCache<FixerRates> usdRatesCache = new LazyMemoryCache<FixerRates>("FixerIO_usdRates");

        private static readonly CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddHours(1))
        };

        private readonly IFixerClient decoratee;

        public FixerClientCachingDecorator(IFixerClient decoratee)
        {
            this.decoratee = decoratee;
        }

        public FixerRates GetUsdRates()
        {
            return usdRatesCache.GetOrAdd("GetUsdRates", (key) => { return decoratee.GetUsdRates(); }, cacheItemPolicy);
        }
    }
}
