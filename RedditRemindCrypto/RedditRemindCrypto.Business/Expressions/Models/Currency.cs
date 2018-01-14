using RedditRemindCrypto.Business.Services.Models;

namespace RedditRemindCrypto.Business.Expressions.Models
{
    public class Currency
    {
        public decimal Amount { get; set; }
        public CurrencyModel Type { get; set; }

        public override string ToString()
        {
            return $"{Amount.ToString()} {Type.Ticker}";
        }
    }
}
