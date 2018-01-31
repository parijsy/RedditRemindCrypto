using RedditRemindCrypto.Business.Interpreters.Enums;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class CommaToken : Token
    {
        public CommaToken(string value)
            : base(TokenType.Comma, value)
        {
        }
    }
}
