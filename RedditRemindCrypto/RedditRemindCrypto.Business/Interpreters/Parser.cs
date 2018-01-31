using RedditRemindCrypto.Business.Interpreters.AbstractSyntaxTree;
using RedditRemindCrypto.Business.Interpreters.Enums;
using RedditRemindCrypto.Business.Interpreters.Exceptions;
using RedditRemindCrypto.Business.Interpreters.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedditRemindCrypto.Business.Interpreters
{
    public class Parser
    {
        private readonly Queue<Token> tokenQueue;
        private Token currentToken;

        public Parser(Lexer lexer, TokenQueueFactory tokenQueueFactory)
        {
            this.tokenQueue = tokenQueueFactory.Create(lexer);
            this.currentToken = tokenQueue.Dequeue();
        }

        public IAbstractSyntaxNode Parse()
        {
            return Expr();
        }

        private IAbstractSyntaxNode Expr()
        {
            var node = Term();
            var operatorTokenType = new TokenType[]
            {
                TokenType.And,
                TokenType.Or
            };

            while (operatorTokenType.Contains(currentToken.Type))
            {
                var token = currentToken;
                switch (token.Type)
                {
                    case TokenType.And:
                    case TokenType.Or:
                    {
                        Eat(token.Type);
                        break;
                    }
                }
                node = new BinaryOperatorNode(node, token, Term());
            }

            return node;
        }

        private IAbstractSyntaxNode Term()
        {
            var node = Factor();
            var operatorTokenType = new TokenType[]
            {
                TokenType.LargerThan,
                TokenType.SmallerThan
            };
            while (operatorTokenType.Contains(currentToken.Type))
            {
                var token = currentToken;
                switch(token.Type)
                {
                    case TokenType.LargerThan:
                    case TokenType.SmallerThan:
                    {
                        Eat(token.Type);
                        break;
                    }
                }
                node = new BinaryOperatorNode(node, token, Factor());
            }

            return node;
        }

        private IAbstractSyntaxNode Factor()
        {
            var token = currentToken;
            switch (token.Type)
            {
                case TokenType.CurrencyAmount:
                {
                    Eat(TokenType.CurrencyAmount);
                    return new NumberNode(token);
                }
                case TokenType.Method:
                {
                    Eat(TokenType.Method);
                    var parameters = GetMethodParameters();
                    return new MethodNode(token, parameters);
                }
                case TokenType.LParen:
                {
                    Eat(TokenType.LParen);
                    var node = Expr();
                    Eat(TokenType.RParen);
                    return node;
                }
                default:
                    throw new InvalidSyntaxException($"Received a {token.GetType().Name} while expecting any of the following tokens: {nameof(TokenType.CurrencyAmount)}, {nameof(TokenType.Method)}, or {nameof(TokenType.LParen)}");
            }
        }

        private ParameterNode[] GetMethodParameters()
        {
            Eat(TokenType.LParen);
            var parameters = new List<ParameterNode>();
            while (currentToken.Type != TokenType.RParen)
            {
                if (currentToken.Type == TokenType.Comma)
                {
                    Eat(TokenType.Comma);
                }
                else
                {
                    parameters.Add(new ParameterNode(currentToken));
                    Eat(currentToken.Type);
                }
            }

            Eat(TokenType.RParen);

            return parameters.ToArray();
        }

        private void Eat(TokenType tokenType)
        {
            if (currentToken.Type != tokenType)
                throw new InvalidSyntaxException($"Tried eating '{currentToken.GetType().Name}'while expecting '{tokenType.GetType().Name}'");

            try
            {
                currentToken = tokenQueue.Dequeue();

            }
            catch (Exception)
            {
                currentToken = new EofToken(null);
            }
        }
    }
}
