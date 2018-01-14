using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditRemindCrypto.Business.Expressions;
using RedditRemindCrypto.Business.Expressions.Enums;
using RedditRemindCrypto.Business.Expressions.Parsers;
using RedditRemindCrypto.Business.UnitTests.Services;

namespace RedditRemindCrypto.Business.UnitTests.Expressions
{
    [TestClass]
    public class ExpressionReaderTests
    {
        private readonly ExpressionReader expressionReader;

        public ExpressionReaderTests()
        {
            var currencyService = new TestCurrencyService();
            var currencyParser = new CurrencyParser(currencyService);
            var expressionParser = new ExpressionOperatorParser();

            this.expressionReader = new ExpressionReader(currencyParser, expressionParser);
        }

        [TestMethod]
        public void ExpressionReader_1()
        {
            var actualExpression = expressionReader.Read("1.5BCH > 2000EUR");

            Assert.AreEqual(1.5m, actualExpression.LeftHandOperator.Amount);
            Assert.AreEqual("BCH", actualExpression.LeftHandOperator.Type.Ticker);
            Assert.AreEqual(ExpressionOperator.LargerThan, actualExpression.Operator);
            Assert.AreEqual(2000, actualExpression.RightHandOperator.Amount);
            Assert.AreEqual("EUR", actualExpression.RightHandOperator.Type.Ticker);
        }
    }
}
