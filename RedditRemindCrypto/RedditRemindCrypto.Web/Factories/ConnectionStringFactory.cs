using RedditRemindCrypto.Business.Factories;

namespace RedditRemindCrypto.Web.Factories
{
    public class ConnectionStringFactory : IConnectionStringFactory
    {
        public string Create()
        {
            return Properties.Settings.Default.ConnectionString;
        }
    }
}