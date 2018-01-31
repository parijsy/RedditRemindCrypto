using RedditRemindCrypto.Business.Interpreters.Enums;
using RedditRemindCrypto.Business.Interpreters.Models;
using System.Collections.Generic;
using System.Linq;

namespace RedditRemindCrypto.Business.Interpreters
{
    public class TokenQueueFactory
    {
        public Queue<Token> Create(Lexer lexer)
        {
            var list = new List<Token>();
            var token = lexer.GetNextToken();
            do
            {
                if (!list.Any())
                {
                    list.Add(token);
                    continue;
                }

                var previousToken = list.Last();
                if (token.Type == TokenType.Currency && previousToken.Type == TokenType.Number)
                {
                    var numberToken = previousToken as NumberToken;
                    var currencyToken = token as CurrencyToken;
                    var currencyAmountToken = new CurrencyAmountToken($"{previousToken.Value}{token.Value}", numberToken, currencyToken);
                    list.Remove(list.Last());
                    list.Add(currencyAmountToken);
                    continue;
                }

                if (token.Type == TokenType.Number && previousToken.Type == TokenType.Currency)
                {
                    var numberToken = token as NumberToken;
                    var currencyToken = previousToken as CurrencyToken;
                    var currencyAmountToken = new CurrencyAmountToken($"{previousToken.Value}{token.Value}", numberToken, currencyToken);
                    list.Remove(list.Last());
                    list.Add(currencyAmountToken);
                    continue;
                }

                list.Add(token);
            }
            while ((token = lexer.GetNextToken()).Type != TokenType.EOF);

            var queue = new Queue<Token>();
            foreach (var item in list)
                queue.Enqueue(item);
            return queue;
        }
    }
}
