namespace RedditRemindCrypto.Business.Settings
{
    public interface IBotSettings
    {
        string Username { get; }
        string Password { get; }
        string ClientId { get; }
        string ClientSecret { get; }
        string RedirectUri { get; }
    }
}
