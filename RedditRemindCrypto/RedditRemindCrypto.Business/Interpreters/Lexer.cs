using RedditRemindCrypto.Business.Extensions;
using RedditRemindCrypto.Business.Interpreters.Exceptions;
using RedditRemindCrypto.Business.Interpreters.Models;
using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedditRemindCrypto.Business.Interpreters
{
    public class Lexer
    {
        private string text;
        private int pos;
        private char? currentChar;

        private static readonly string[] methodNames = new string[] { "Price", "MarketCap", "Volume", "HasRankOrHigher", "Before", "After" };
        private readonly Lazy<IEnumerable<CurrencyModel>> currencies;

        public Lexer(string text, ICurrencyService currencyService)
        {
            this.text = text;
            pos = 0;
            currentChar = string.IsNullOrWhiteSpace(text) ? (char?)null : text[pos];

            this.currencies = new Lazy<IEnumerable<CurrencyModel>>(() => currencyService.GetAll());
        }

        public Token GetNextToken()
        {
            while (currentChar.HasValue)
            {
                if (currentChar.Value.IsSpace())
                {
                    SkipWhitespace();
                    continue;
                }
                if (currentChar.Value.IsNumber())
                    return Number();

                if (currentChar.Value.IsLetter() || currentChar.Value.IsCurrencySymbol())
                    return HandleLetter();

                switch (currentChar.Value)
                {
                    case '&':
                        return AndOperator();
                    case '|':
                        return OrOperator();
                    case '<':
                        return SmallerThan(currentChar.Value.ToString());
                    case '>':
                        return LargerThan(currentChar.Value.ToString());
                    case '(':
                        return LParen(currentChar.Value.ToString());
                    case ')':
                        return RParen(currentChar.Value.ToString());
                    case ',':
                        return Comma(currentChar.Value.ToString());
                    case '"':
                        return String();
                    default:
                        throw new InvalidCharacterException($"Unable to interpret the character '{currentChar.Value}'");
                }
            }
            return new EofToken(null);
        }

        private void Advance()
        {
            pos++;
            if (pos >= text.Length)
                currentChar = null;
            else
                currentChar = text[pos];
        }

        private void SkipWhitespace()
        {
            while (currentChar.HasValue && currentChar.Value.IsSpace())
                Advance();
        }

        private NumberToken Number()
        {
            var result = string.Empty;
            while (currentChar.HasValue && (currentChar.Value.IsNumber() || currentChar == '_'))
            {
                if (currentChar != '_')
                    result += currentChar;
                Advance();
            }

            if (currentChar == '.')
            {
                result += currentChar;
                Advance();
                while (currentChar.HasValue && (currentChar.Value.IsNumber() || currentChar == '_'))
                {
                    if (currentChar != '_')
                        result += currentChar;
                    Advance();
                }

            }
            return new NumberToken(result);
        }

        private Token HandleLetter()
        {
            var result = string.Empty;
            while (currentChar.HasValue && (currentChar.Value.IsLetter() || currentChar.Value == '-' || currentChar.Value.IsCurrencySymbol()))
            {
                result += currentChar;
                Advance();
            }

            if (methodNames.Any(x => x.Equals(result, StringComparison.InvariantCultureIgnoreCase)))
                return new MethodToken(result);

            foreach (var currency in currencies.Value)
            {
                if (currency.GetAllNames().Any(x => x.Equals(result, StringComparison.InvariantCultureIgnoreCase)))
                    return new CurrencyToken(result, currency.Ticker, currency.CurrencyType);
            }

            throw new InvalidCharacterException($"Unable to parse '{result}' to a {nameof(MethodToken)} or {nameof(CurrencyToken)}");
        }

        private AndToken AndOperator()
        {
            var result = string.Empty;
            result += currentChar;
            Advance();

            if (currentChar.HasValue && currentChar == '&')
            {
                result += currentChar;
                Advance();
                return new AndToken(result);
            }

            throw new InvalidCharacterException($"Unable to parse '{result}' to a {nameof(AndToken)}");
        }

        private OrToken OrOperator()
        {
            var result = string.Empty;
            result += currentChar;
            Advance();

            if (currentChar.HasValue && currentChar == '|')
            {
                result += currentChar;
                Advance();
                return new OrToken(result);
            }

            throw new InvalidCharacterException($"Unable to parse '{result}' to a {nameof(OrToken)}");
        }

        private StringToken String()
        {
            if (currentChar.Value != '"')
                throw new InvalidCharacterException($"Unable to parse to a {nameof(StringToken)}, first character must be double quote, instead of '{currentChar.Value}'");

            Advance();

            var result = string.Empty;
            while(currentChar.HasValue && currentChar.Value != '"')
            {
                result += currentChar;
                Advance();
            }

            if (!currentChar.HasValue)
                throw new InvalidCharacterException($"Unable to parse to a {nameof(StringToken)}, missing the closing double quote");

            Advance();
            return new StringToken(result);
        }

        private SmallerThanToken SmallerThan(string value)
        {
            Advance();
            return new SmallerThanToken(value);
        }

        private LargerThanToken LargerThan(string value)
        {
            Advance();
            return new LargerThanToken(value);
        }

        private LParenToken LParen(string value)
        {
            Advance();
            return new LParenToken(value);
        }

        private RParenToken RParen(string value)
        {
            Advance();
            return new RParenToken(value);
        }

        private CommaToken Comma(string value)
        {
            Advance();
            return new CommaToken(value);
        }
    }
}
