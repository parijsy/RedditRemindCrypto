using RedditRemindCrypto.Business.Interpreters.Enums;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class OrToken : Token
    {
        public OrToken(string value)
            : base(TokenType.Or, value)
        {
        }
    }
}
