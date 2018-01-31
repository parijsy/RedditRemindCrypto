using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.Services.Enums;
using RedditRemindCrypto.Business.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedditRemindCrypto.Business.UnitTests.Services
{
    public class TestCurrencyService : ICurrencyService
    {
        private static readonly List<CurrencyModel> collection = new List<CurrencyModel>
        {
            new CurrencyModel { CurrencyType = CurrencyType.Fiat, Ticker = "EUR", AlternativeNames = new List<string> { "€", "euro" }, FixerIOName = "EUR" },
            new CurrencyModel { CurrencyType = CurrencyType.Fiat, Ticker = "USD", AlternativeNames = new List<string> { "$", "dollar" }, FixerIOName = "USD" },
            new CurrencyModel { CurrencyType = CurrencyType.Fiat, Ticker = "GBP", AlternativeNames = new List<string> { "£", "pound" }, FixerIOName = "GBP" },
            new CurrencyModel { CurrencyType = CurrencyType.Fiat, Ticker = "JPY", AlternativeNames = new List<string> { "¥", "yen" }, FixerIOName = "JPY" },

            new CurrencyModel { CurrencyType = CurrencyType.Crypto, Ticker = "BTC", AlternativeNames = new List<string> { "bitcoin" }, CoinMarketCapId = "bitcoin" },
            new CurrencyModel { CurrencyType = CurrencyType.Crypto, Ticker = "BCH", AlternativeNames = new List<string> { "bitcoincash", "bitcoin-cash" }, CoinMarketCapId = "bitcoin-cash" },
            new CurrencyModel { CurrencyType = CurrencyType.Crypto, Ticker = "LTC", AlternativeNames = new List<string> { "litecoin" }, CoinMarketCapId = "litecoin" },
            new CurrencyModel { CurrencyType = CurrencyType.Crypto, Ticker = "VTC", AlternativeNames = new List<string> { "vertcoin" }, CoinMarketCapId = "vertcoin" },
        };

        public CurrencyModel GetByTicker(string ticker)
        {
            return collection.Single(x => x.Ticker == ticker);
        }

        public IEnumerable<CurrencyModel> GetAll()
        {
            return collection;
        }

        public void Add(string ticker, string coinMarketCapId)
        {
            throw new NotImplementedException();
        }

        public void AddAlternativeName(string ticker, string alternativeName)
        {
            throw new NotImplementedException();
        }
    }
}
