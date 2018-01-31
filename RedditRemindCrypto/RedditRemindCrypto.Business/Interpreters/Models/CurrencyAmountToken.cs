using RedditRemindCrypto.Business.Interpreters.Enums;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public class CurrencyAmountToken : Token
    {
        public NumberToken Number { get; private set; }
        public CurrencyToken Currency { get; private set; }

        public CurrencyAmountToken(string value, NumberToken numberToken, CurrencyToken currencyToken)
            : base(TokenType.CurrencyAmount, value)
        {
            Number = numberToken;
            Currency = currencyToken;
        }
    }
}
