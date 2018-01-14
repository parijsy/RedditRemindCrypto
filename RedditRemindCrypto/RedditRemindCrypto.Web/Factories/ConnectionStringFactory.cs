using RedditRemindCrypto.Business.Factories;
using System.Configuration;

namespace RedditRemindCrypto.Web.Factories
{
    public class ConnectionStringFactory : IConnectionStringFactory
    {
        private readonly string connectionstring;

        public ConnectionStringFactory()
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