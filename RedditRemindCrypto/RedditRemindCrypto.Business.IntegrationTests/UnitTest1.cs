using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditRemindCrypto.Business.Clients.CoinMarketCap;
using RedditRemindCrypto.Business.Clients.CoinMarketCap.Decorators;
using RedditRemindCrypto.Business.Clients.FixerIO;
using RedditRemindCrypto.Business.Clients.FixerIO.Decorators;
using RedditRemindCrypto.Business.Expressions;
using RedditRemindCrypto.Business.Expressions.Converters;
using RedditRemindCrypto.Business.Expressions.Enums;
using RedditRemindCrypto.Business.Expressions.Models;
using RedditRemindCrypto.Business.Expressions.Parsers;
using RedditRemindCrypto.Business.Factories;
using RedditRemindCrypto.Business.IntegrationTests.Factories;
using RedditRemindCrypto.Business.IntegrationTests.Settings;
using RedditRemindCrypto.Business.Interpreters;
using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.Services.Models;
using System.Diagnostics;
using System.Linq;

namespace RedditRemindCrypto.Business.IntegrationTests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly ICurrencyService currencyService;
        private readonly IRemindRequestService remindRequestService;
        private readonly ExpressionEvaluator expressionEvaluator;
        private readonly RedditUnreadMessagesReader unreadMessageReader;
        private readonly RemindRequestHandler remindRequestHandler;

        public UnitTest1()
        {
            var settings = new TestBotSettings();
            var connectionStringFactory = new TestConnectionStringFactory();
            var coinmarkcapClient = new CoinMarketCapClientCachingDecorator(new CoinMarketCapClient());
            var fixerClient = new FixerClientCachingDecorator(new FixerClient());
            var currencyConverter = new CurrencyConverter(coinmarkcapClient, fixerClient);
            var redditClientFactory = new RedditClientFactory();

            this.expressionEvaluator = new ExpressionEvaluator(currencyConverter);
            this.currencyService = new CurrencyService(connectionStringFactory);
            this.remindRequestService = new RemindRequestService(connectionStringFactory);
            var currencyParser = new CurrencyParser(currencyService);
            var expressionParser = new ExpressionOperatorParser();
            var expressionReader = new ExpressionReader(currencyParser, expressionParser);
            var tokenConverter = new TokenConverter(currencyConverter, currencyService, coinmarkcapClient);
            var tokenQueueFactory = new TokenQueueFactory();
            var interpreterFactory = new InterpreterFactory(tokenConverter, currencyService, tokenQueueFactory);
            var expressionExtractor = new ExpressionExtractor(interpreterFactory);
            this.unreadMessageReader = new RedditUnreadMessagesReader(settings, remindRequestService, expressionExtractor, redditClientFactory);
            this.remindRequestHandler = new RemindRequestHandler(settings, remindRequestService, expressionReader, expressionEvaluator, interpreterFactory, redditClientFactory);
        }

        [TestMethod]
        public void ExpressionEvaluator()
        {
            var expression = new Expression
            {
                LeftHandOperator = new Currency { Amount = 1.0005m, Type = currencyService.GetByTicker("BCH") },
                Operator = ExpressionOperator.SmallerThan,
                RightHandOperator = new Currency { Amount = 1, Type = currencyService.GetByTicker("BTC") }
            };
            var result = expressionEvaluator.Evaluate(expression);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ExpressionEvaluator2()
        {
            var expression = new Expression
            {
                LeftHandOperator = new Currency { Amount = 1, Type = currencyService.GetByTicker("USD") },
                Operator = ExpressionOperator.SmallerThan,
                RightHandOperator = new Currency { Amount = 1, Type = currencyService.GetByTicker("EUR") }
            };
            var result = expressionEvaluator.Evaluate(expression);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void REDDIT_FindAndRemindTest()
        {
            //unreadMessageReader.ReadUnreadComments();
            //unreadMessageReader.ReadUnreadPrivateMessages();

            //var requests = remindRequestService.GetAll();
            //remindRequestHandler.Handle(requests);
        }

        [TestMethod]
        public void CurrencyServiceTest()
        {
            var currencies = currencyService.GetAll();

            foreach (var currency in currencies)
            {
                Debug.WriteLine(currency);
            }
        }

        [TestMethod]
        public void RemindRequestServiceTest()
        {
            var request = new RemindRequest()
            {
                Expression = "AddAndDeleteTest",
                Permalink = "somelink",
                User = "parijsy"
            };
            remindRequestService.Save(request);
            var all = remindRequestService.GetAll();
            remindRequestService.Delete(all.First(x => x.Expression == "AddAndDeleteTest"));
        }
    }
}
