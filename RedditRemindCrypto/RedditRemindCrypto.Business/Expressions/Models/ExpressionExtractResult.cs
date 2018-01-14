using System.Collections.Generic;

namespace RedditRemindCrypto.Business.Expressions.Models
{
    public class ExpressionExtractResult
    {
        public ICollection<string> Expressions { get; set; }
        public ICollection<string> InvalidExpressions { get; set; }

        public ExpressionExtractResult()
        {
            Expressions = new List<string>();
            InvalidExpressions = new List<string>();
        }
    }
}
