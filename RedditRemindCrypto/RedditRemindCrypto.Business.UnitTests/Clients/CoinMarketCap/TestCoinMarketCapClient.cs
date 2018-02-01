using RedditRemindCrypto.Business.Clients.CoinMarketCap;
using RedditRemindCrypto.Business.Clients.CoinMarketCap.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedditRemindCrypto.Business.UnitTests.Clients.CoinMarketCap
{
    public class TestCoinMarketCapClient : ICoinMarketCapClient
    {
        private static readonly CoinMarketCapTicker[] data = new CoinMarketCapTicker[]
        {
            new CoinMarketCapTicker
            {
                Id = "bitcoin",
                Name = "Bitcoin",
                Symbol = "BCH",
                Rank = 1,
                Price_usd = 8812.28m,
                Price_btc = 1.0m,
                Volume = 8804820000.0m,
                Market_cap_usd = 148392626604m
            },
            new CoinMarketCapTicker
            {
                Id = "ethereum",
                Name = "Ethereum",
                Symbol = "ETH",
                Rank = 2,
                Price_usd = 984.819m,
                Price_btc = 0.113955m,
                Volume = 4636190000.0m,
                Market_cap_usd = 95872771074.0m
            },
            new CoinMarketCapTicker
            {
                Id = "ripple",
                Name = "Ripple",
                Symbol = "XRP",
                Rank = 3,
                Price_usd = 0.931775m,
                Price_btc = 0.00010782m,
                Volume = 1227060000.0m,
                Market_cap_usd = 36347812087.0m
            },
            new CoinMarketCapTicker
            {
                Id = "bitcoin-cash",
                Name = "Bitcoin Cash",
                Symbol = "BCH",
                Rank = 4,
                Price_usd = 1214.88m,
                Price_btc = 0.140576m,
                Volume = 613902000.0m,
                Market_cap_usd = 20585093766.0m
            },
            new CoinMarketCapTicker
            {
                Id = "cardano",
                Name = "Cardano",
                Symbol = "ADA",
                Rank = 5,
                Price_usd = 0.412045m,
                Price_btc = 0.00004768m,
                Volume = 528835000.0m,
                Market_cap_usd = 10683119780.0m
            },
            new CoinMarketCapTicker
            {
                Id = "vertcoin",
                Name = "Vertcoin",
                Symbol = "VTC",
                Rank = 122,
                Price_usd = 3.05097m,
                Price_btc = 0.00035036m,
                Volume = 2613910.0m,
                Market_cap_usd = 130403263.0m
            },
        };


        public CoinMarketCapTicker Ticker(string coinMarketCapId)
        {
            return data.Single(x => x.Id == coinMarketCapId);
        }

        public IEnumerable<CoinMarketCapTicker> TopCoins(int limit)
        {
            throw new NotImplementedException();
        }
    }
}
