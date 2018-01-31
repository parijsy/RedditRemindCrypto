using RedditRemindCrypto.Business.Interpreters.Enums;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class SmallerThanToken : Token
    {
        public SmallerThanToken(string value)
            : base(TokenType.SmallerThan, value)
        {
        }
    }
}
