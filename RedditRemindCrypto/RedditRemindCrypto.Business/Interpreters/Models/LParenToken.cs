using RedditRemindCrypto.Business.Interpreters.Enums;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class LParenToken : Token
    {
        public LParenToken(string value)
            : base(TokenType.LParen, value)
        {
        }
    }
}
