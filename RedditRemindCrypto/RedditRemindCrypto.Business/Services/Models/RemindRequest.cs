using System;

namespace RedditRemindCrypto.Business.Services.Models
{
    public class RemindRequest
    {
        public Guid Id { get; set; }
        public string Expression { get; set; }
        public string User { get; set; }
        public string Permalink { get; set; }
    }
}
