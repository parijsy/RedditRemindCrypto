using RedditRemindCrypto.Business.Expressions.Enums;
using RedditRemindCrypto.Business.Expressions.Models;
using RedditRemindCrypto.Business.Expressions.Parsers;
using System;

namespace RedditRemindCrypto.Business.Expressions
{
    public class ExpressionReader
    {
        private readonly CurrencyParser currencyParser;
        private readonly ExpressionOperatorParser expressionOperatorParser;

        public ExpressionReader(CurrencyParser currencyParser, ExpressionOperatorParser expressionParser)
        {
            this.currencyParser = currencyParser;
            this.expressionOperatorParser = expressionParser;
        }

        public Expression Read(string expression)
        {
            var parts = expression.Split(' ');
            if (parts.Length < 3)
                throw new InvalidOperationException($"'{expression}' is not a valid expression");

            var leftHandCurrency = ReadCurrency(parts[0]);
            var expressionOperator = ReadOperator(parts[1]);
            var rightHandCurrency = ReadCurrency(parts[2]);


            return new Expression
            {
                LeftHandOperator = leftHandCurrency,
                Operator = expressionOperator,
                RightHandOperator = rightHandCurrency
            };
        }

        public bool TryRead(string expression, out Expression result)
        {
            try
            {
                result = Read(expression);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        private Currency ReadCurrency(string toParse)
        {
            Currency result;
            if (currencyParser.TryParse(toParse, out result))
                return result;

            throw new InvalidOperationException($"Unable to parse '{toParse}' to the type {nameof(Currency)}");
        }

        private ExpressionOperator ReadOperator(string toParse)
        {
            ExpressionOperator result;
            if (expressionOperatorParser.TryParse(toParse, out result))
                return result;

            throw new InvalidOperationException($"Unable to parse '{toParse}' to the type {nameof(ExpressionOperator)}");
        }
    }
}