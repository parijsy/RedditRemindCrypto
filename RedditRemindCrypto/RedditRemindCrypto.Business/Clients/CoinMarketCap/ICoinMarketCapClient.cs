using RedditRemindCrypto.Business.Clients.CoinMarketCap.Models;

namespace RedditRemindCrypto.Business.Clients.CoinMarketCap
{
    public interface ICoinMarketCapClient
    {
        CoinMarketCapTicker Ticker(string coinMarketCapId);
    }
}
