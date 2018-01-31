using RedditRemindCrypto.Business.Interpreters.AbstractSyntaxTree;

namespace RedditRemindCrypto.Business.Interpreters
{
    public class NodeVisitor
    {
        public object Visit(IAbstractSyntaxNode node)
        {
            var methodName = $"Visit{node.GetType().Name}";
            var type = this.GetType();
            var methodInfo = type.GetMethod(methodName);
            return methodInfo.Invoke(this, new object[] { node });
        }
    }
}
