using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditRemindCrypto.Business.Clients.CoinMarketCap;
using System.Linq;

namespace RedditRemindCrypto.Business.IntegrationTests.Clients.CoinMarketCap
{
    [TestClass]
    public class CoinMarketCapClientTest
    {
        private readonly CoinMarketCapClient client;

        public CoinMarketCapClientTest()
        {
            this.client = new CoinMarketCapClient();
        }

        [TestMethod]
        public void CoinMarketCapClient_GetLimitAmount()
        {
            var limit = 5;
            var results = client.TopCoins(limit);

            Assert.AreEqual(limit, results.Count());
        }
    }
}
