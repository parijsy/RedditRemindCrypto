using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditRemindCrypto.Business.Expressions;
using RedditRemindCrypto.Business.Expressions.Converters;
using RedditRemindCrypto.Business.Interpreters;
using RedditRemindCrypto.Business.UnitTests.Clients.CoinMarketCap;
using RedditRemindCrypto.Business.UnitTests.Clients.FixerIO;
using RedditRemindCrypto.Business.UnitTests.Services;

namespace RedditRemindCrypto.Business.UnitTests.Expressions
{
    [TestClass]
    public class ExpressionExtractorTests
    {
        private readonly ExpressionExtractor expressionExtractor;

        public ExpressionExtractorTests()
        {
            var currencyService = new TestCurrencyService();

            var tokenQueueFactory = new TokenQueueFactory();
            var coinMarketCapClient = new TestCoinMarketCapClient();
            var fixerClient = new TestFixerClient();
            var currencyConverter = new CurrencyConverter(coinMarketCapClient, fixerClient);
            var tokenConverter = new TokenConverter(currencyConverter, currencyService, coinMarketCapClient);
            var interpreterFactory = new InterpreterFactory(tokenConverter, currencyService, tokenQueueFactory);
            this.expressionExtractor = new ExpressionExtractor(interpreterFactory);
        }

        [TestMethod]
        public void ExpressionExtractor_1()
        {
            var expected = "1VTC > 100EUR";
            var message = $"Some Text. /u/RemindCryptoBot {expected};";
            var actual = expressionExtractor.Extract(message);

            Assert.AreEqual(1, actual.Expressions.Count);
            Assert.AreEqual(0, actual.InvalidExpressions.Count);
            Assert.IsTrue(actual.Expressions.Contains(expected));
        }

        [TestMethod]
        public void ExpressionExtractor_2()
        {
            var message = "Some Text. /u/RemindCryptoBot lorem ipsum";
            var actual = expressionExtractor.Extract(message);

            Assert.AreEqual(0, actual.Expressions.Count);
            Assert.AreEqual(0, actual.InvalidExpressions.Count);
        }

        [TestMethod]
        public void ExpressionExtractor_3()
        {
            var expected = "InvalidExpression";
            var message = $"Some Text. /u/RemindCryptoBot {expected};";
            var actual = expressionExtractor.Extract(message);

            Assert.AreEqual(0, actual.Expressions.Count);
            Assert.AreEqual(1, actual.InvalidExpressions.Count);
            Assert.IsTrue(actual.InvalidExpressions.Contains(expected));
        }

        [TestMethod]
        public void ExpressionExtractor_4()
        {
            var expected1 = "1VTC < 3EUR";
            var expected2 = "1VTC > 100EUR";
            var message = $"/u/RemindCryptoBot {expected1}; Some Text. /u/RemindCryptoBot {expected2};";
            var actual = expressionExtractor.Extract(message);

            Assert.AreEqual(2, actual.Expressions.Count);
            Assert.AreEqual(0, actual.InvalidExpressions.Count);
            Assert.IsTrue(actual.Expressions.Contains(expected1));
            Assert.IsTrue(actual.Expressions.Contains(expected2));
        }

        [TestMethod]
        public void ExpressionExtractor_5()
        {
            var expected1 = "1VTC < 3EUR";
            var message = $"/u/RemindCryptoBot {expected1}\nSome Text.";
            var actual = expressionExtractor.Extract(message);

            Assert.AreEqual(1, actual.Expressions.Count);
            Assert.AreEqual(0, actual.InvalidExpressions.Count);
            Assert.IsTrue(actual.Expressions.Contains(expected1));
        }
    }
}
