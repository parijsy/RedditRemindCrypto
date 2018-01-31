using RedditRemindCrypto.Business.Interpreters.Models;

namespace RedditRemindCrypto.Business.Interpreters.AbstractSyntaxTree
{
    public class ParameterNode : IAbstractSyntaxNode
    {
        public readonly Token Token;

        public ParameterNode(Token token)
        {
            this.Token = token;
        }
    }
}
