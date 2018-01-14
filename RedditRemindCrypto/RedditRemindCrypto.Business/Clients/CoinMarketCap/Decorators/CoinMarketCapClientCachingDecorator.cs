using RedditRemindCrypto.Business.Caching;
using RedditRemindCrypto.Business.Clients.CoinMarketCap.Models;
using System;
using System.Runtime.Caching;

namespace RedditRemindCrypto.Business.Clients.CoinMarketCap.Decorators
{
    public class CoinMarketCapClientCachingDecorator : ICoinMarketCapClient
    {
        private static readonly LazyMemoryCache<CoinMarketCapTicker> tickerCache = new LazyMemoryCache<CoinMarketCapTicker>("CoinMarketCap_Ticker");

        private static readonly CacheItemPolicy tickerCacheItemPolicy = new CacheItemPolicy
        {
            SlidingExpiration = new TimeSpan(hours: 0, minutes: 10, seconds: 0)
        };

        private readonly ICoinMarketCapClient decoratee;

        public CoinMarketCapClientCachingDecorator(ICoinMarketCapClient decoratee)
        {
            this.decoratee = decoratee;
        }

        public CoinMarketCapTicker Ticker(string coinMarketCapId)
        {
            return tickerCache.GetOrAdd(coinMarketCapId, decoratee.Ticker, tickerCacheItemPolicy);
        }
    }
}
