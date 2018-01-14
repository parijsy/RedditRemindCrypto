using RedditRemindCrypto.Business.Services.Models;
using System.Collections.Generic;

namespace RedditRemindCrypto.Business.Services
{
    public interface ICurrencyService
    {
        CurrencyModel GetByTicker(string ticker);
        IEnumerable<CurrencyModel> GetAll();
    }
}
