using RedditRemindCrypto.Business.Caching;
using RedditRemindCrypto.Business.Clients.FixerIO.Models;

namespace RedditRemindCrypto.Business.Clients.FixerIO.Decorators
{
    public class FixerClientCachingDecorator : IFixerClient
    {
        private static readonly LazyMemoryCache<FixerRates> usdRatesCache = new LazyMemoryCache<FixerRates>("FixerIO_usdRates");

        private readonly IFixerClient decoratee;

        public FixerClientCachingDecorator(IFixerClient decoratee)
        {
            this.decoratee = decoratee;
        }

        public FixerRates GetUsdRates()
        {
            return usdRatesCache.GetOrAdd("GetUsdRates", (key) => { return decoratee.GetUsdRates(); });
        }
    }
}
