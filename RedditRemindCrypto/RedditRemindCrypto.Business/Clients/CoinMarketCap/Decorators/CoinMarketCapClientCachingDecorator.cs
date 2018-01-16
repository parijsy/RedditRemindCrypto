using RedditRemindCrypto.Business.Caching;
using RedditRemindCrypto.Business.Clients.CoinMarketCap.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace RedditRemindCrypto.Business.Clients.CoinMarketCap.Decorators
{
    public class CoinMarketCapClientCachingDecorator : ICoinMarketCapClient
    {
        private static readonly LazyMemoryCache<CoinMarketCapTicker> tickerCache = new LazyMemoryCache<CoinMarketCapTicker>("CoinMarketCap_Ticker");

        private static readonly CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(1))
        };

        private readonly ICoinMarketCapClient decoratee;

        public CoinMarketCapClientCachingDecorator(ICoinMarketCapClient decoratee)
        {
            this.decoratee = decoratee;
        }

        public CoinMarketCapTicker Ticker(string coinMarketCapId)
        {
            return tickerCache.GetOrAdd(coinMarketCapId, decoratee.Ticker, cacheItemPolicy);
        }

        public IEnumerable<CoinMarketCapTicker> TopCoins(int limit)
        {
            // Not caching this
            return decoratee.TopCoins(limit);
        }
    }
}
