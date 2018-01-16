using RedditRemindCrypto.Business.Clients.CoinMarketCap;
using RedditRemindCrypto.Business.Services;
using System;
using System.Diagnostics;
using System.Linq;

namespace RedditRemindCrypto.Business
{
    public class AutoPopularCoinAdder
    {
        private readonly ICurrencyService currencyService;
        private readonly ICoinMarketCapClient coinMarketCapClient;

        public AutoPopularCoinAdder(ICurrencyService currencyService, ICoinMarketCapClient coinMarketCapClient)
        {
            this.currencyService = currencyService;
            this.coinMarketCapClient = coinMarketCapClient;
        }

        public void AutoAddPopularCoins(int limit)
        {
            var topCoins = coinMarketCapClient.TopCoins(limit);
            var allCoins = currencyService.GetAll();

            foreach(var topCoin in topCoins)
            {
                if (allCoins.Any(x => x.Ticker == topCoin.Symbol))
                    continue;

                try
                {
                    currencyService.Add(topCoin.Symbol, topCoin.Id);
                    currencyService.AddAlternativeName(topCoin.Symbol, topCoin.Id);
                }
                catch(Exception e)
                {
                    Trace.TraceError(e.Message);
                }
            }
        }
    }
}
