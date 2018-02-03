using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditRemindCrypto.Business.Expressions.Converters;
using RedditRemindCrypto.Business.Interpreters;
using RedditRemindCrypto.Business.Interpreters.Models;
using RedditRemindCrypto.Business.Services.Enums;
using RedditRemindCrypto.Business.UnitTests.Clients.CoinMarketCap;
using RedditRemindCrypto.Business.UnitTests.Clients.FixerIO;
using RedditRemindCrypto.Business.UnitTests.Services;

namespace RedditRemindCrypto.Business.UnitTests.Interpreters
{
    [TestClass]
    public class TokenConverterTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenTokenConverter_WhenHasRankOrHigherWithFiat_ThenFail()
        {
            var numberToken = new NumberToken("4");
            var currencyToken = new CurrencyToken("EUR", "EUR", CurrencyType.Fiat);
            var tokenConverter = CreateTokenConverter();

            var result = tokenConverter.HasRankOrHigher(numberToken, currencyToken);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenTokenConverter_WhenHasMarketCapWithFiat_ThenFail()
        {
            var currencyToken = new CurrencyToken("EUR", "EUR", CurrencyType.Fiat);
            var tokenConverter = CreateTokenConverter();

            var result = tokenConverter.ToUsdMarketCap(currencyToken);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenTokenConverter_WhenVolumeWithFiat_ThenFail()
        {
            var currencyToken = new CurrencyToken("EUR", "EUR", CurrencyType.Fiat);
            var tokenConverter = CreateTokenConverter();

            var result = tokenConverter.ToUsdVolume(currencyToken);
            Assert.Fail();
        }

        private TokenConverter CreateTokenConverter()
        {
            var coinMarketCapClient = new TestCoinMarketCapClient();
            var fixerClient = new TestFixerClient();
            var currencyService = new TestCurrencyService();
            var currencyConverter = new CurrencyConverter(coinMarketCapClient, fixerClient);
            return new TokenConverter(currencyConverter, currencyService, coinMarketCapClient, fixerClient);
        }
    }
}
