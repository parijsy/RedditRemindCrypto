using System.Collections.Generic;
using System.Linq;

namespace RedditRemindCrypto.Business.Services.Models
{
    public static class CurrencyModelExtensions
    {
        public static IEnumerable<string> GetAllNames(this CurrencyModel model)
        {
            return model.AlternativeNames.Concat(new string[] { model.Ticker });
        }
    }
}
