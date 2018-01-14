using Newtonsoft.Json;
using RedditRemindCrypto.Business.Clients.CoinMarketCap.Models;
using System.Linq;
using System.Net.Http;

namespace RedditRemindCrypto.Business.Clients.CoinMarketCap
{
    public class CoinMarketCapClient : ICoinMarketCapClient
    {
        public CoinMarketCapTicker Ticker(string coinMarketCapId)
        {
            using (var client = new HttpClient())
            {
                var json = client.GetStringAsync($"https://api.coinmarketcap.com/v1/ticker/{coinMarketCapId}/").Result;
                return JsonConvert.DeserializeObject<CoinMarketCapTicker[]>(json).First();
            }
        }
    }
}
