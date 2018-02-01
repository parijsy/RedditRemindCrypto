using RedditRemindCrypto.Business.Expressions.Models;
using RedditRemindCrypto.Business.Interpreters;
using System;

namespace RedditRemindCrypto.Business.Expressions
{
    public class ExpressionExtractor
    {
        private readonly InterpreterFactory interpreterFactory;

        public ExpressionExtractor(InterpreterFactory interpreterFactory)
        {
            this.interpreterFactory = interpreterFactory;
        }

        public ExpressionExtractResult Extract(string message)
        {
            var result = new ExpressionExtractResult();
            var parts = message.Split(new string[] { "/u/RemindCryptoBot" }, StringSplitOptions.None);
            for (var i = 1; i < parts.Length; ++i)
            {
                var part = parts[i];
                var expressionEnd = part.IndexOf(';');
                if (expressionEnd == -1)
                    expressionEnd = part.IndexOf('\n');

                if (expressionEnd == -1)
                    continue;

                var expression = part.Substring(0, expressionEnd).TrimStart();
                try
                {
                    var interpreter = interpreterFactory.Create(expression);
                    var interpreterResult = interpreter.Interpret();
                    result.Expressions.Add(expression);
                }
                catch (Exception)
                {
                    result.InvalidExpressions.Add(expression);
                }
            }

            return result;
        }
    }
}
