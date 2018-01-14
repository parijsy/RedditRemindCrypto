using Newtonsoft.Json;
using RedditRemindCrypto.Business.Clients.FixerIO.Models;
using System.Net.Http;

namespace RedditRemindCrypto.Business.Clients.FixerIO
{
    public class FixerClient : IFixerClient
    {
        public FixerRates GetUsdRates()
        {
            using (var client = new HttpClient())
            {
                var json = client.GetStringAsync($"https://api.fixer.io/latest?base=USD").Result;
                return JsonConvert.DeserializeObject<FixerRates>(json);
            }
        }
    }
}
