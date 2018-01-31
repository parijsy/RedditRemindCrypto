using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditRemindCrypto.Business.Clients.CoinMarketCap;
using RedditRemindCrypto.Business.Clients.FixerIO;
using RedditRemindCrypto.Business.Expressions.Converters;
using RedditRemindCrypto.Business.IntegrationTests.Factories;
using RedditRemindCrypto.Business.Interpreters;
using RedditRemindCrypto.Business.Services;

namespace RedditRemindCrypto.Business.IntegrationTests.Interpreters
{
    [TestClass]
    public class InterpreterTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var text = "1BCH > 1EUR && (1BTC > 100USD || 10LTC < 0.1GBP)";
            var interpreter = CreateInterpreter(text);

            var result = interpreter.Interpret();
            Assert.IsTrue(result.Result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var text = "HasRankOrHigher(4, BCH) && Price(BCH) > 1$ && MarketCap(BCH) > 10_000_000€ && Volume(BCH) > 1_000USD";
            var interpreter = CreateInterpreter(text);

            var result = interpreter.Interpret();
            Assert.IsTrue(result.Result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var text = @"Before(""2500-12-31"")";
            var interpreter = CreateInterpreter(text);

            var result = interpreter.Interpret();
            Assert.IsTrue(result.Result);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var text = @"After(""2017-12-31"")";
            var interpreter = CreateInterpreter(text);

            var result = interpreter.Interpret();
            Assert.IsTrue(result.Result);
        }

        [TestMethod]
        public void GivenInterpreter_WhenExpressionIsAlwaysFalse_ThenDetectAlwaysFalse()
        {
            var text = @"Before(""2017-12-31"") && After(""2018-12-31"")";
            var interpreter = CreateInterpreter(text);

            var result = interpreter.Interpret();
            Assert.IsFalse(result.Result);
            Assert.IsTrue(result.IsAlwaysFalse.Value);
        }

        private Interpreter CreateInterpreter(string text)
        {
            var connectionStringFactory = new TestConnectionStringFactory();
            var currencyService = new CurrencyService(connectionStringFactory);
            var lexer = new Lexer(text, currencyService);
            var tokenQueueFactory = new TokenQueueFactory();
            var parser = new Parser(lexer, tokenQueueFactory);

            var coinMarketCapClient = new CoinMarketCapClient();
            var fixerClient = new FixerClient();
            var currencyConverter = new CurrencyConverter(coinMarketCapClient, fixerClient);
            var currencyAmountTokenConverter = new TokenConverter(currencyConverter, currencyService, coinMarketCapClient);

            return new Interpreter(parser, currencyAmountTokenConverter);
        }
    }
}
