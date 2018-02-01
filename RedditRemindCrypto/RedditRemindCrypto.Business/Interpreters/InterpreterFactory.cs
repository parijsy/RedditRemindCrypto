using RedditRemindCrypto.Business.Services;

namespace RedditRemindCrypto.Business.Interpreters
{
    public class InterpreterFactory
    {
        private readonly TokenConverter tokenConverter;
        private readonly ICurrencyService currencyService;
        private readonly TokenQueueFactory tokenQueueFactory;

        public InterpreterFactory(TokenConverter tokenConverter, ICurrencyService currencyService, TokenQueueFactory tokenQueueFactory)
        {
            this.tokenConverter = tokenConverter;
            this.currencyService = currencyService;
            this.tokenQueueFactory = tokenQueueFactory;
        }

        public Interpreter Create(string text)
        {
            var lexer = new Lexer(text, currencyService);
            var parser = new Parser(lexer, tokenQueueFactory);
            return new Interpreter(parser , tokenConverter);
        }
    }
}
