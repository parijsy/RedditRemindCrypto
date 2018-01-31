using RedditRemindCrypto.Business.Interpreters.Models;

namespace RedditRemindCrypto.Business.Interpreters.AbstractSyntaxTree
{
    public class BinaryOperatorNode : IAbstractSyntaxNode
    {
        public readonly IAbstractSyntaxNode Left;
        private Token token;
        public readonly Token Op;
        public readonly IAbstractSyntaxNode Right;

        public BinaryOperatorNode(IAbstractSyntaxNode left, Token op, IAbstractSyntaxNode right)
        {
            this.Left = left;
            this.token = this.Op = op;
            this.Right = right;
        }
    }
}
