using RedditRemindCrypto.Business.Clients.FixerIO;
using RedditRemindCrypto.Business.Clients.FixerIO.Models;
using System;
using System.Collections.Generic;

namespace RedditRemindCrypto.Business.UnitTests.Clients.FixerIO
{
    public class TestFixerClient : IFixerClient
    {
        public FixerRates GetUsdRates()
        {
            return new FixerRates
            {
                Base = "USD",
                Date = DateTime.Now,
                Rates = new Dictionary<string, decimal>
                {
                    { "AUD", 1.2495m },
                    { "BGN", 1.5698m },
                    { "BRL", 3.1694m },
                    { "CAD", 1.2319m },
                    { "CHF", 0.93129m },
                    { "CNY", 6.2967m },
                    { "CZK", 20.28m },
                    { "DKK", 5.9738m },
                    { "EUR", 0.80263m },
                    { "GBP", 0.70246m },
                    { "HKD", 7.82m },
                    { "HRK", 5.9656m },
                    { "HUF", 248.86m },
                    { "IDR", 13418m },
                    { "ILS", 3.4284m },
                    { "INR", 64.023m },
                    { "ISK", 100.34m },
                    { "JPY", 109.66m },
                    { "KRW", 1071.1m },
                    { "MXN", 18.566m },
                    { "MYR", 3.904m },
                    { "NOK", 7.6816m },
                    { "NZD", 1.36m },
                    { "PHP", 51.633m },
                    { "PLN", 3.3344m },
                    { "RON", 3.7361m },
                    { "RUB", 56.282m },
                    { "SEK", 7.8682m },
                    { "SGD", 1.3125m },
                    { "THB", 31.36m },
                    { "TRY", 3.7464m },
                    { "ZAR", 11.898m }
                }
            };
        }
    }
}
