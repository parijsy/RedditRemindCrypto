using RedditRemindCrypto.Business.Interpreters.Enums;
using RedditRemindCrypto.Business.Services.Enums;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class CurrencyToken : Token
    {
        public CurrencyType CurrencyType { get; private set; }
        public string Ticker { get; private set; }

        public CurrencyToken(string value, string ticker, CurrencyType currencyType)
            : base(TokenType.Currency, value)
        {
            Ticker = ticker;
            CurrencyType = currencyType;
        }
    }
}
