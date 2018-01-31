using RedditRemindCrypto.Business.Interpreters.Models;

namespace RedditRemindCrypto.Business.Interpreters.AbstractSyntaxTree
{
    public class MethodNode : IAbstractSyntaxNode
    {
        public readonly Token Token;
        public readonly ParameterNode[] Parameters;

        public MethodNode(Token token, ParameterNode[] parameters)
        {
            this.Token = token;
            this.Parameters = parameters;
        }
    }
}
