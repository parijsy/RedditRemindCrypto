using RedditRemindCrypto.Business.Expressions.Models;
using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.Services.Models;
using System;
using System.Globalization;

namespace RedditRemindCrypto.Business.Expressions.Parsers
{
    public class CurrencyParser
    {
        private readonly ICurrencyService currencyService;

        public CurrencyParser(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        public bool TryParse(string toParse, out Currency result)
        {
            foreach (var currency in currencyService.GetAll())
            {
                foreach (var name in currency.GetAllNames())
                {
                    if (!toParse.EndsWith(name, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    decimal amount;
                    var culture = CultureInfo.CreateSpecificCulture("en-GB");
                    var amountString = toParse.Replace(name, "");
                    if (!decimal.TryParse(amountString, NumberStyles.Number, culture, out amount))
                        throw new InvalidCastException($"Unable to cast {amountString} to a {nameof(Decimal)}");

                    result = new Currency
                    {
                        Type = currency,
                        Amount = amount
                    };
                    return true;
                }
            }
            result = null;
            return false;
        }
    }
}
