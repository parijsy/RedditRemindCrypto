using RedditRemindCrypto.Business.Settings;

namespace RedditRemindCrypto.Business.IntegrationTests.Settings
{
    public class TestBotSettings : IBotSettings
    {
        public string Username => Properties.Settings.Default.RedditBot_Username;
        public string Password => Properties.Settings.Default.RedditBot_Password;
        public string ClientId => Properties.Settings.Default.RedditBot_ClientId;
        public string ClientSecret => Properties.Settings.Default.RedditBot_ClientSecret;
        public string RedirectUri => Properties.Settings.Default.RedditBot_RedirectUri;
    }
}
