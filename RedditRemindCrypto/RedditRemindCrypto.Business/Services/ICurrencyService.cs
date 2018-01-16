using RedditRemindCrypto.Business.Services.Models;
using System.Collections.Generic;

namespace RedditRemindCrypto.Business.Services
{
    public interface ICurrencyService
    {
        void Add(string ticker, string coinMarketCapId);
        void AddAlternativeName(string ticker, string alternativeName);
        CurrencyModel GetByTicker(string ticker);
        IEnumerable<CurrencyModel> GetAll();
    }
}
