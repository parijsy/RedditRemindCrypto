using RedditRemindCrypto.Business.Interpreters.Enums;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class StringToken : Token
    {
        public StringToken(string value)
            : base(TokenType.String, value)
        {
        }
    }
}
