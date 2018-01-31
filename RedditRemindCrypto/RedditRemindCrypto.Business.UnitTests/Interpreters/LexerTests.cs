using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedditRemindCrypto.Business.Interpreters;
using RedditRemindCrypto.Business.Interpreters.Enums;
using RedditRemindCrypto.Business.Interpreters.Models;
using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.Services.Enums;
using RedditRemindCrypto.Business.UnitTests.Services;

namespace RedditRemindCrypto.Business.UnitTests.Interpreters
{
    [TestClass]
    public class LexerTests
    {
        private readonly ICurrencyService currencyService;

        public LexerTests()
        {
            this.currencyService = new TestCurrencyService();
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsNull_ThenEof()
        {
            var lexer = CreateLexer(null);
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.EOF, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsEmptyString_ThenEof()
        {
            var lexer = CreateLexer(string.Empty);
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.EOF, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsIntegerNumber_ThenNumber()
        {
            var expectedNumber = "42";
            var lexer = CreateLexer(expectedNumber);
            var token = lexer.GetNextToken() as NumberToken;

            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual(expectedNumber, token.Value);
            Assert.AreEqual(42m, token.NumericValue);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsFloatingPointNumber_ThenNumber()
        {
            var expectedNumber = "3.14";
            var lexer = CreateLexer(expectedNumber);
            var token = lexer.GetNextToken() as NumberToken;

            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual(expectedNumber, token.Value);
            Assert.AreEqual(3.14m, token.NumericValue);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsNumberWithUnderscoreSeparators_ThenNumber()
        {
            var expectedNumber = "1_000_000.000_1";
            var lexer = CreateLexer(expectedNumber);
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual(expectedNumber.Replace("_", string.Empty), token.Value);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsAndOperator_ThenAnd()
        {
            var lexer = CreateLexer("&&");
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.And, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsOrOperator_ThenOr()
        {
            var lexer = CreateLexer("||");
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.Or, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsSmallerThan_ThenSmallerThan()
        {
            var lexer = CreateLexer("<");
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.SmallerThan, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsLargerThan_ThenLargerThan()
        {
            var lexer = CreateLexer(">");
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.LargerThan, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsLParen_ThenLParen()
        {
            var lexer = CreateLexer("(");
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.LParen, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsRParen_ThenRParen()
        {
            var lexer = CreateLexer(")");
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.RParen, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsMethodName_ThenMethod()
        {
            var lexer = CreateLexer("Before");
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.Method, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsMethodName_ThenIgnoreCase()
        {
            var lexer = CreateLexer("BeFoRe");
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.Method, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsMethodNameAfter_ThenMethodIsAfter()
        {
            var lexer = CreateLexer("After");
            var token = lexer.GetNextToken() as MethodToken;

            Assert.AreEqual(Method.After, token.Method);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsMethodNameBefore_ThenMethodIsBefore()
        {
            var lexer = CreateLexer("Before");
            var token = lexer.GetNextToken() as MethodToken;

            Assert.AreEqual(Method.Before, token.Method);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsMethodNameMarketCap_ThenMethodIsMarketCap()
        {
            var lexer = CreateLexer("MarketCap");
            var token = lexer.GetNextToken() as MethodToken;

            Assert.AreEqual(Method.MarketCap, token.Method);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsMethodNamePrice_ThenMethodIsPrice()
        {
            var lexer = CreateLexer("Price");
            var token = lexer.GetNextToken() as MethodToken;

            Assert.AreEqual(Method.Price, token.Method);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsMethodNameHasRankOrHigher_ThenMethodIsHasRankOrHigher()
        {
            var lexer = CreateLexer("HasRankOrHigher");
            var token = lexer.GetNextToken() as MethodToken;

            Assert.AreEqual(Method.HasRankOrHigher, token.Method);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsMethodNameVolume_ThenMethodIsVolume()
        {
            var lexer = CreateLexer("Volume");
            var token = lexer.GetNextToken() as MethodToken;

            Assert.AreEqual(Method.Volume, token.Method);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsCurrency_ThenCurrency()
        {
            var lexer = CreateLexer("EUR");
            var token = lexer.GetNextToken() as CurrencyToken;

            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual(CurrencyType.Fiat, token.CurrencyType);
            Assert.AreEqual("EUR", token.Ticker);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsCurrency_ThenIgnoreCase()
        {
            var lexer = CreateLexer("eUr");
            var token = lexer.GetNextToken() as CurrencyToken;

            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual(CurrencyType.Fiat, token.CurrencyType);
            Assert.AreEqual("EUR", token.Ticker);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsCurrency_ThenAllowHyphen()
        {
            var lexer = CreateLexer("bitcoin-cash");
            var token = lexer.GetNextToken() as CurrencyToken;

            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual(CurrencyType.Crypto, token.CurrencyType);
            Assert.AreEqual("BCH", token.Ticker);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsCurrencySymbol_ThenCurrency()
        {
            var lexer = CreateLexer("€");
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.Currency, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsComma_ThenComma()
        {
            var lexer = CreateLexer(",");
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.Comma, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenTextIsString_ThenString()
        {
            var lexer = CreateLexer(@"""HelloWorld""");
            var token = lexer.GetNextToken();

            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual("HelloWorld", token.Value);
        }

        [TestMethod]
        public void GivenLexer_WhenSimpleExpression_ThenSuccess()
        {
            var lexer = CreateLexer("2LTC > 300EUR");

            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual("2", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual("LTC", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LargerThan, token.Type);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual("300", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual("EUR", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.EOF, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenPriceMethodExpression_ThenSuccess()
        {
            var lexer = CreateLexer("Price(LTC) > 150EUR");

            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Method, token.Type);
            Assert.AreEqual("Price", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LParen, token.Type);
            Assert.AreEqual("(", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual("LTC", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.RParen, token.Type);
            Assert.AreEqual(")", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LargerThan, token.Type);
            Assert.AreEqual(">", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual("150", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual("EUR", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.EOF, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenMarketCapMethodExpression_ThenSuccess()
        {
            var lexer = CreateLexer("MarketCap(BTC) > 153_000_000_000$");

            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Method, token.Type);
            Assert.AreEqual("MarketCap", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LParen, token.Type);
            Assert.AreEqual("(", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual("BTC", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.RParen, token.Type);
            Assert.AreEqual(")", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LargerThan, token.Type);
            Assert.AreEqual(">", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual("153000000000", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual("$", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.EOF, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenVolumeMethodExpression_ThenSuccess()
        {
            var lexer = CreateLexer("Volume(BCH) > 10BTC");

            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Method, token.Type);
            Assert.AreEqual("Volume", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LParen, token.Type);
            Assert.AreEqual("(", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual("BCH", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.RParen, token.Type);
            Assert.AreEqual(")", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LargerThan, token.Type);
            Assert.AreEqual(">", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual("10", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual("BTC", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.EOF, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenHasRankOrHigherMethodExpression_ThenSuccess()
        {
            var lexer = CreateLexer("HasRankOrHigher(20, BCH)");

            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Method, token.Type);
            Assert.AreEqual("HasRankOrHigher", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LParen, token.Type);
            Assert.AreEqual("(", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual("20", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Comma, token.Type);
            Assert.AreEqual(",", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Currency, token.Type);
            Assert.AreEqual("BCH", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.RParen, token.Type);
            Assert.AreEqual(")", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.EOF, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenBeforeMethodExpression_ThenSuccess()
        {
            var lexer = CreateLexer(@"Before(""2019"")");

            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Method, token.Type);
            Assert.AreEqual("Before", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LParen, token.Type);
            Assert.AreEqual("(", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual("2019", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.RParen, token.Type);
            Assert.AreEqual(")", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.EOF, token.Type);
        }

        [TestMethod]
        public void GivenLexer_WhenAfterMethodExpression_ThenSuccess()
        {
            var lexer = CreateLexer(@"After(""2019"")");

            var token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.Method, token.Type);
            Assert.AreEqual("After", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.LParen, token.Type);
            Assert.AreEqual("(", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual("2019", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.RParen, token.Type);
            Assert.AreEqual(")", token.Value);

            token = lexer.GetNextToken();
            Assert.AreEqual(TokenType.EOF, token.Type);
        }

        private Lexer CreateLexer(string text)
        {
            return new Lexer(text, currencyService);
        }
    }
}
