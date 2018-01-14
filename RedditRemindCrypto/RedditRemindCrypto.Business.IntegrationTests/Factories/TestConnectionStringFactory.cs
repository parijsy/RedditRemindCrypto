using RedditRemindCrypto.Business.Factories;

namespace RedditRemindCrypto.Business.IntegrationTests.Factories
{
    public class TestConnectionStringFactory : IConnectionStringFactory
    {
        public string Create()
        {
            return Properties.Settings.Default.ConnectionString;
        }
    }
}
