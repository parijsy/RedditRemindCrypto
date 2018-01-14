using RedditRemindCrypto.Business.Factories;
using System.Configuration;

namespace RedditRemindCrypto.Business.IntegrationTests.Factories
{
    public class TestConnectionStringFactory : IConnectionStringFactory
    {
        private readonly string connectionstring;

        public TestConnectionStringFactory()
        {
            var reader = new AppSettingsReader();
            connectionstring = reader.GetValue("ConnectionString", typeof(string)) as string;
        }

        public string Create()
        {
            return connectionstring;
        }
    }
}
