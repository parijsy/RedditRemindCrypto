using RedditRemindCrypto.Business.Expressions.Converters;
using RedditRemindCrypto.Business.Expressions.Enums;
using RedditRemindCrypto.Business.Expressions.Models;

namespace RedditRemindCrypto.Business.Expressions
{
    public class ExpressionEvaluator
    {
        private readonly CurrencyConverter currencyConverter;

        public ExpressionEvaluator(CurrencyConverter currencyConverter)
        {
            this.currencyConverter = currencyConverter;
        }

        public bool Evaluate(Expression expression)
        {
            var leftHandInUSD = currencyConverter.ToUSD(expression.LeftHandOperator);
            var rightHandInUSD = currencyConverter.ToUSD(expression.RightHandOperator);

            if (expression.Operator == ExpressionOperator.LargerThan)
                return leftHandInUSD > rightHandInUSD;

            return leftHandInUSD < rightHandInUSD;
        }
    }
}
