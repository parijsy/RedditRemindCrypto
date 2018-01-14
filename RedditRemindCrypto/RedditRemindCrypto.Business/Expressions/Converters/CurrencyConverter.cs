using RedditRemindCrypto.Business.Clients.CoinMarketCap;
using RedditRemindCrypto.Business.Clients.FixerIO;
using RedditRemindCrypto.Business.Expressions.Models;
using RedditRemindCrypto.Business.Services.Models;

namespace RedditRemindCrypto.Business.Expressions.Converters
{
    public class CurrencyConverter
    {
        private readonly ICoinMarketCapClient coinmarkcapClient;
        private readonly IFixerClient fixerClient;

        public CurrencyConverter(ICoinMarketCapClient coinmarkcapClient, IFixerClient fixerClient)
        {
            this.coinmarkcapClient = coinmarkcapClient;
            this.fixerClient = fixerClient;
        }

        public decimal ToUSD(Currency currency)
        {
            if (currency.Type.Ticker == "USD")
                return currency.Amount;

            switch (currency.Type.CurrencyType)
            {
                case CurrencyType.Fiat:
                    return ConvertFiatToUSD(currency);
                default:
                    return ConvertCryptoToUSD(currency);
            }
        }

        private decimal ConvertFiatToUSD(Currency currency)
        {
            var rates = fixerClient.GetUsdRates();
            var usdValue = rates.Rates[currency.Type.FixerIOName];
            return currency.Amount * (1 / usdValue);
        }

        public decimal ConvertCryptoToUSD(Currency currency)
        {
            var ticker = coinmarkcapClient.Ticker(currency.Type.CoinMarketCapId);
            return currency.Amount * ticker.Price_usd;
        }
    }
}
