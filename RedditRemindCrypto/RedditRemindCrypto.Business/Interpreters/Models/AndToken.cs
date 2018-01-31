using RedditRemindCrypto.Business.Interpreters.Enums;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class AndToken : Token
    {
        public AndToken(string value)
            : base(TokenType.And, value)
        {
        }
    }
}
