using RedditRemindCrypto.Business.Interpreters.Enums;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class RParenToken : Token
    {
        public RParenToken(string value)
            : base(TokenType.RParen, value)
        {
        }
    }
}
