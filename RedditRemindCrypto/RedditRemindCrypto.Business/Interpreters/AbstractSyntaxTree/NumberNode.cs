using RedditRemindCrypto.Business.Interpreters.Models;

namespace RedditRemindCrypto.Business.Interpreters.AbstractSyntaxTree
{
    public class NumberNode : IAbstractSyntaxNode
    {
        public readonly Token token;

        public NumberNode(Token token)
        {
            this.token = token;
        }
    }
}
