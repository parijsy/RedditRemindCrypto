using RedditRemindCrypto.Business.Clients.FixerIO.Models;

namespace RedditRemindCrypto.Business.Clients.FixerIO
{
    public interface IFixerClient
    {
        FixerRates GetUsdRates();
    }
}
