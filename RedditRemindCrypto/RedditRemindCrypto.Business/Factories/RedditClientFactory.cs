using RedditRemindCrypto.Business.Settings;
using RedditSharp;

namespace RedditRemindCrypto.Business.Factories
{
    public class RedditClientFactory
    {
        public Reddit Create(IBotSettings settings)
        {
            var webAgent = new BotWebAgent(settings.Username, settings.Password, settings.ClientId, settings.ClientSecret, settings.RedirectUri);
            //This will check if the access token is about to expire before each request and automatically request a new one for you  
            //"false" means that it will NOT load the logged in user profile so reddit.User will be null  
            var reddit = new Reddit(webAgent, false);
            reddit.InitOrUpdateUser();
            return reddit;
        }
    }
}
