using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditRemindCrypto.Business.Expressions;
using RedditRemindCrypto.Business.Expressions.Parsers;
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
            var currencyParser = new CurrencyParser(currencyService);
            var expressionParser = new ExpressionOperatorParser();
            var expressionReader = new ExpressionReader(currencyParser, expressionParser);

            this.expressionExtractor = new ExpressionExtractor(expressionReader);
        }

        [TestMethod]
        public void ExpressionExtractor_1()
        {
            var expected = "1VTC > 100EUR";
            var message = $"Some Text. /u/RemindCryptoBot ({expected})";
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
            var message = $"Some Text. /u/RemindCryptoBot ({expected})";
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
            var message = $"/u/RemindCryptoBot ({expected1}) Some Text. /u/RemindCryptoBot ({expected2})";
            var actual = expressionExtractor.Extract(message);

            Assert.AreEqual(2, actual.Expressions.Count);
            Assert.AreEqual(0, actual.InvalidExpressions.Count);
            Assert.IsTrue(actual.Expressions.Contains(expected1));
            Assert.IsTrue(actual.Expressions.Contains(expected2));
        }
    }
}
