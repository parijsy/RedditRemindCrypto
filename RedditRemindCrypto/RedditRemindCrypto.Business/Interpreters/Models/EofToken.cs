using RedditRemindCrypto.Business.Interpreters.Enums;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class EofToken : Token
    {
        public EofToken(string value)
            : base(TokenType.EOF, value)
        {
        }
    }
}
