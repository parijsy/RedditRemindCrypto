using RedditRemindCrypto.Business.Expressions.Enums;

namespace RedditRemindCrypto.Business.Expressions.Models
{
    public class Expression
    {
        public Currency LeftHandOperator { get; set; }
        public ExpressionOperator Operator { get; set; }
        public Currency RightHandOperator { get; set; }
    }
}
