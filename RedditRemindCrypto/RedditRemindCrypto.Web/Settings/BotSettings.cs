using RedditRemindCrypto.Business.Settings;
using System.Configuration;

namespace RedditRemindCrypto.Web.Settings
{
    public class BotSettings : IBotSettings
    {
        private readonly AppSettingsReader reader;

        public BotSettings()
        {
            this.reader = new AppSettingsReader();
        }

        public string Username => (string)reader.GetValue("RedditBot_Username", typeof(string));
        public string Password => (string)reader.GetValue("RedditBot_Password", typeof(string));
        public string ClientId => (string)reader.GetValue("RedditBot_ClientId", typeof(string));
        public string ClientSecret => (string)reader.GetValue("RedditBot_ClientSecret", typeof(string));
        public string RedirectUri => (string)reader.GetValue("RedditBot_RedirectUri", typeof(string));
    }
}