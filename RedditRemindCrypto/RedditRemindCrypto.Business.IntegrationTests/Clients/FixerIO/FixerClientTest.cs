using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditRemindCrypto.Business.Clients.FixerIO;

namespace RedditRemindCrypto.Business.IntegrationTests.Clients.FixerIO
{
    [TestClass]
    public class FixerClientTest
    {
        private readonly FixerClient client;

        public FixerClientTest()
        {
            this.client = new FixerClient();
        }

        [TestMethod]
        public void GivenFixerClient_WhenGetRates_ThenSuccess()
        {
            var result = client.GetUsdRates();

            Assert.AreEqual("USD", result.Base);
            Assert.IsTrue(result.Rates.Count > 0);
        }
    }
}
