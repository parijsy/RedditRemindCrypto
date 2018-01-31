using RedditRemindCrypto.Business.Interpreters.Enums;
using System;

namespace RedditRemindCrypto.Business.Interpreters.Models
{
    public sealed class MethodToken : Token
    {
        public Method Method { get; private set; }

        public MethodToken(string value)
            : base(TokenType.Method, value)
        {
            Method = GetMethodFromValue();
        }

        private Method GetMethodFromValue()
        {
            switch (Value.ToLower())
            {
                case "price":
                    return Method.Price;
                case "marketcap":
                    return Method.MarketCap;
                case "volume":
                    return Method.Volume;
                case "hasrankorhigher":
                    return Method.HasRankOrHigher;
                case "before":
                    return Method.Before;
                case "after":
                    return Method.After;
                default:
                    throw new InvalidOperationException($"'{Value}' is not a valid '{nameof(MethodToken)}' method.");
            }
        }
    }
}
