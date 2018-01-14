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
    }
}
