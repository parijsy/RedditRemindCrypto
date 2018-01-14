using RedditRemindCrypto.Business.Expressions.Models;
using System;

namespace RedditRemindCrypto.Business.Expressions
{
    public class ExpressionExtractor
    {
        private readonly ExpressionReader expressionReader;

        public ExpressionExtractor(ExpressionReader expressionReader)
        {
            this.expressionReader = expressionReader;
        }

        public ExpressionExtractResult Extract(string message)
        {
            var result = new ExpressionExtractResult();
            var parts = message.Split(new string[] { "/u/RemindCryptoBot" }, StringSplitOptions.None);
            for (var i = 1; i < parts.Length; ++i)
            {
                var part = parts[i];
                var expressionStart = part.IndexOf('(');
                var expressionEnd = part.IndexOf(')');

                if (expressionStart == -1 || expressionEnd == -1)
                    continue;

                Expression expressionResult;
                var expression = part.Substring(expressionStart + 1, expressionEnd - expressionStart - 1);
                if (expressionReader.TryRead(expression, out expressionResult))
                {
                    result.Expressions.Add(expression);
                }
                else
                {
                    result.InvalidExpressions.Add(expression);
                }
            }

            return result;
        }
    }
}
