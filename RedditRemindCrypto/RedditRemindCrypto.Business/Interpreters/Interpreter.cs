using RedditRemindCrypto.Business.Interpreters.AbstractSyntaxTree;
using RedditRemindCrypto.Business.Interpreters.Enums;
using RedditRemindCrypto.Business.Interpreters.Exceptions;
using RedditRemindCrypto.Business.Interpreters.Models;
using System.Linq;

namespace RedditRemindCrypto.Business.Interpreters
{
    public class Interpreter : NodeVisitor
    {
        private readonly Parser parser;
        private readonly TokenConverter tokenConverter;

        public Interpreter(Parser parser, TokenConverter tokenConverter)
        {
            this.parser = parser;
            this.tokenConverter = tokenConverter;
        }

        public bool Interpret()
        {
            var tree = parser.Parse();
            return (bool)Visit(tree);
        }

        public bool VisitBinOp(BinaryOperatorNode node)
        {
            switch (node.Op.Type)
            {
                // Ipv boolean terug geven een object teruggeven? { Result = true/false, AlwaysFalse = true/false }
                // operator overload implementeren voor object die nieuw object teruggeeft en properties juist instelt.
                // Interpret zou dit object terug kunnen geven, of een exception opgooien als AlwaysFalse true is, zodat dit teruggekoppelt kan worden.
                case TokenType.And:
                    return (bool)Visit(node.Left) && (bool)Visit(node.Right);
                case TokenType.Or:
                    return (bool)Visit(node.Left) || (bool)Visit(node.Right);
                case TokenType.LargerThan:
                    return (decimal)Visit(node.Left) > (decimal)Visit(node.Right);
                case TokenType.SmallerThan:
                    return (decimal)Visit(node.Left) < (decimal)Visit(node.Right);
                default:
                    throw new InvalidSyntaxException("Error syntax");
            }
        }

        public object VisitMethodOp(MethodNode node)
        {
            var methodToken = node.Token as MethodToken;
            switch (methodToken.Method)
            {
                case Method.After:
                    {
                        if (node.Parameters.Count() != 1)
                            throw new InvalidSyntaxException($"Method '{nameof(Method.MarketCap)}' received an invalid amount of parameters");

                        var stringToken = node.Parameters[0].Token as StringToken;
                        return tokenConverter.IsAfterDateTime(stringToken);
                    }
                case Method.Before:
                    {
                        if (node.Parameters.Count() != 1)
                            throw new InvalidSyntaxException($"Method '{nameof(Method.MarketCap)}' received an invalid amount of parameters");

                        var stringToken = node.Parameters[0].Token as StringToken;
                        return tokenConverter.IsBeforeDateTime(stringToken);
                    }
                case Method.MarketCap:
                    {
                        if (node.Parameters.Count() != 1)
                            throw new InvalidSyntaxException($"Method '{nameof(Method.MarketCap)}' received an invalid amount of parameters");

                        var currencyToken = node.Parameters[0].Token as CurrencyToken;
                        return tokenConverter.ToUsdMarketCap(currencyToken);
                    }
                case Method.Price:
                    {
                        if (node.Parameters.Count() != 1)
                            throw new InvalidSyntaxException($"Method '{nameof(Method.Price)}' received an invalid amount of parameters");

                        var currencyToken = node.Parameters[0].Token as CurrencyToken;
                        return tokenConverter.ToUsdPrice(currencyToken);
                    }
                case Method.TopPrice:
                    {
                        if (node.Parameters.Count() != 2)
                            throw new InvalidSyntaxException($"Method '{nameof(Method.TopPrice)}' received an invalid amount of parameters");

                        var rank = node.Parameters[0].Token as NumberToken;
                        var currencyToken = node.Parameters[1].Token as CurrencyToken;
                        return tokenConverter.HasRankOrHigher(rank, currencyToken);
                    }
                case Method.Volume:
                    {
                        if (node.Parameters.Count() != 1)
                            throw new InvalidSyntaxException($"Method '{nameof(Method.Volume)}' received an invalid amount of parameters");

                        var currencyToken = node.Parameters[0].Token as CurrencyToken;
                        return tokenConverter.ToUsdVolume(currencyToken);
                    }
                default:
                    throw new InvalidSyntaxException("Error syntax");
            }
        }

        public decimal VisitNum(NumberNode node)
        {
            switch (node.token.Type)
            {
                case TokenType.CurrencyAmount:
                    return tokenConverter.ToUSD(node.token as CurrencyAmountToken);
                default:
                    throw new InvalidSyntaxException("Error syntax");
            }
        }
    }
}
