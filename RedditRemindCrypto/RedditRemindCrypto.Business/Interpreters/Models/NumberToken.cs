using RedditRemindCrypto.Business.Interpreters.Enums;
using System.Globalization;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class NumberToken : Token
    {
        private static NumberFormatInfo numberInfo = CultureInfo.CreateSpecificCulture("en-US").NumberFormat;

        public decimal NumericValue { get; private set; }

        public NumberToken(string value)
            : base(TokenType.Number, value)
        {
            NumericValue = decimal.Parse(value, numberInfo);
        }
    }
}
