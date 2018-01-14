using System.Collections.Generic;

namespace RedditRemindCrypto.Business.Services.Models
{
    public class CurrencyModel
    {
        public CurrencyType CurrencyType { get; set; }
        public string Ticker { get; set; }
        public IEnumerable<string> AlternativeNames { get; set; }
        public string FixerIOName { get; set; }
        public string CoinMarketCapId { get; set; }
    }
}
