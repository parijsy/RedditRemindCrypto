using Newtonsoft.Json;

namespace RedditRemindCrypto.Business.Clients.CoinMarketCap.Models
{
    public class CoinMarketCapTicker
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int Rank { get; set; }
        public decimal Price_usd { get; set; }
        public decimal Price_btc { get; set; }
        public decimal Market_cap_usd { get; set; }

        [JsonProperty("24h_volume_usd")]
        public decimal Volume { get; set; }
    }
}
