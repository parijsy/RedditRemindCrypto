using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditRemindCrypto.Business.Interpreters;
using RedditRemindCrypto.Business.Interpreters.Enums;
using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.UnitTests.Services;

namespace RedditRemindCrypto.Business.UnitTests.Interpreters
{
    [TestClass]
    public class TokenQueueFactoryTests
    {
        private readonly ICurrencyService currencyService;

        public TokenQueueFactoryTests()
        {
            this.currencyService = new TestCurrencyService();
        }

        [TestMethod]
        public void GivenTokenQueueFactory_WhenDequeue_ThenCorrectOrder()
        {
            var lexer = CreateLexer(@"1 > ""2""");
            var tokenQueueFactory = new TokenQueueFactory();
            var queue = tokenQueueFactory.Create(lexer);

            var token = queue.Dequeue();
            Assert.AreEqual(TokenType.Number, token.Type);

            token = queue.Dequeue();
            Assert.AreEqual(TokenType.LargerThan, token.Type);

            token = queue.Dequeue();
            Assert.AreEqual(TokenType.String, token.Type);
        }

        [TestMethod]
        public void GivenTokenQueueFactory_WhenCurrencyBeforeNumber_ThenCurrencyAmount()
        {
            var lexer = CreateLexer("€10");
            var tokenQueueFactory = new TokenQueueFactory();
            var queue = tokenQueueFactory.Create(lexer);

            var token = queue.Dequeue();
            Assert.AreEqual(TokenType.CurrencyAmount, token.Type);
        }

        [TestMethod]
        public void GivenTokenQueueFactory_WhenNumberBeforeCurrency_ThenCurrencyAmount()
        {
            var lexer = CreateLexer("10EUR");
            var tokenQueueFactory = new TokenQueueFactory();
            var queue = tokenQueueFactory.Create(lexer);

            var token = queue.Dequeue();
            Assert.AreEqual(TokenType.CurrencyAmount, token.Type);
        }

        [TestMethod]
        public void GivenTokenQueueFactory_WhenMultipleMerge_ThenSuccess()
        {
            var lexer = CreateLexer("€1 < 1BTC");
            var tokenQueueFactory = new TokenQueueFactory();
            var queue = tokenQueueFactory.Create(lexer);

            var token = queue.Dequeue();
            Assert.AreEqual(TokenType.CurrencyAmount, token.Type);

            token = queue.Dequeue();
            Assert.AreEqual(TokenType.SmallerThan, token.Type);

            token = queue.Dequeue();
            Assert.AreEqual(TokenType.CurrencyAmount, token.Type);
        }

        private Lexer CreateLexer(string text)
        {
            return new Lexer(text, currencyService);
        }
    }
}
