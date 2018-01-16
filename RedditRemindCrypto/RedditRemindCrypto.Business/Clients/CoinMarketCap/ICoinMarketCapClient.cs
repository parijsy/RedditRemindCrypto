using RedditRemindCrypto.Business.Clients.CoinMarketCap.Models;
using System.Collections.Generic;

namespace RedditRemindCrypto.Business.Clients.CoinMarketCap
{
    public interface ICoinMarketCapClient
    {
        CoinMarketCapTicker Ticker(string coinMarketCapId);
        IEnumerable<CoinMarketCapTicker> TopCoins(int limit);
    }
}
