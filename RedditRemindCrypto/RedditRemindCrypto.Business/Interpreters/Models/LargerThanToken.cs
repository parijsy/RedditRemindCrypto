using RedditRemindCrypto.Business.Interpreters.Enums;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class LargerThanToken : Token
    {
        public LargerThanToken(string value)
            : base(TokenType.LargerThan, value)
        {
        }
    }
}
