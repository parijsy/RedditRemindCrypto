using RedditRemindCrypto.Business.Services.Models;
using System.Collections.Generic;

namespace RedditRemindCrypto.Web.Models.Home
{
    public class SupportedCurrenciesModel
    {
        public IEnumerable<CurrencyModel> FiatCurrencies { get; set; }
        public IEnumerable<CurrencyModel> CryptoCurrencies { get; set; }
    }
}