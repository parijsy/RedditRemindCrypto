﻿using RedditRemindCrypto.Business.Clients.CoinMarketCap;
using RedditRemindCrypto.Business.Clients.FixerIO;
using RedditRemindCrypto.Business.Expressions.Converters;
using RedditRemindCrypto.Business.Expressions.Models;
using RedditRemindCrypto.Business.Interpreters.Models;
using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.Services.Enums;
using System;
using System.Globalization;

namespace RedditRemindCrypto.Business.Interpreters
{
    public class TokenConverter
    {
        private readonly CurrencyConverter currencyConverter;
        private readonly ICurrencyService currencyService;
        private readonly ICoinMarketCapClient coinMarketCapClient;
        private readonly IFixerClient fixerClient;

        public TokenConverter(CurrencyConverter currencyConverter, ICurrencyService currencyService, ICoinMarketCapClient coinMarketCapClient, IFixerClient fixerClient)
        {
            this.currencyConverter = currencyConverter;
            this.currencyService = currencyService;
            this.coinMarketCapClient = coinMarketCapClient;
            this.fixerClient = fixerClient;
        }

        public decimal ToUSD(CurrencyAmountToken token)
        {
            return currencyConverter.ToUSD(new Currency
            {
                Amount = token.Number.NumericValue,
                Type = currencyService.GetByTicker(token.Currency.Ticker)
            });
        }

        public decimal ToUsdVolume(CurrencyToken currencyToken)
        {
            if (currencyToken.CurrencyType == CurrencyType.Fiat)
                throw new ArgumentException("MarketCap only accepts crypto currencies.");

            var currency = currencyService.GetByTicker(currencyToken.Ticker);
            var info = coinMarketCapClient.Ticker(currency.CoinMarketCapId);
            return info.Volume;
        }

        public decimal ToUsdPrice(CurrencyToken currencyToken)
        {
            if (currencyToken.CurrencyType == CurrencyType.Fiat)
            {
                var rates = fixerClient.GetUsdRates();
                return rates.Rates[currencyToken.Ticker];
            }
            else
            {
                var currency = currencyService.GetByTicker(currencyToken.Ticker);
                var info = coinMarketCapClient.Ticker(currency.CoinMarketCapId);
                return info.Price_usd;
            }
        }

        public decimal ToUsdMarketCap(CurrencyToken currencyToken)
        {
            if (currencyToken.CurrencyType == CurrencyType.Fiat)
                throw new ArgumentException("MarketCap only accepts crypto currencies.");

            var currency = currencyService.GetByTicker(currencyToken.Ticker);
            var info = coinMarketCapClient.Ticker(currency.CoinMarketCapId);
            return info.Market_cap_usd;
        }

        public InterpreterResult HasRankOrHigher(NumberToken numberToken, CurrencyToken currencyToken)
        {
            var expectedRank = numberToken.NumericValue;
            if (expectedRank < 1)
                throw new ArgumentOutOfRangeException("Rank must be 1 or higher");

            if (currencyToken.CurrencyType == CurrencyType.Fiat)
                throw new ArgumentException("HasRankOrHigher only accepts crypto currencies.");

            var currency = currencyService.GetByTicker(currencyToken.Ticker);
            var info = coinMarketCapClient.Ticker(currency.CoinMarketCapId);
            return new InterpreterResult(expectedRank <= info.Rank, null);
        }

        public InterpreterResult IsAfterDateTime(StringToken token)
        {
            var culture = new CultureInfo("en-US");
            var date = DateTime.Parse(token.Value, culture);
            return new InterpreterResult(date< DateTime.Now, null);
        }

        public InterpreterResult IsBeforeDateTime(StringToken token)
        {
            var culture = new CultureInfo("en-US");
            var date = DateTime.Parse(token.Value, culture);
            var result = date > DateTime.Now;
            return new InterpreterResult(result, result ? (bool?)null : true);
        }
    }
}
