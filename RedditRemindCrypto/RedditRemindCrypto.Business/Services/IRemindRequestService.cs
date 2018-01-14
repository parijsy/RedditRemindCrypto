using RedditRemindCrypto.Business.Services.Models;
using System.Collections.Generic;

namespace RedditRemindCrypto.Business.Services
{
    public interface IRemindRequestService
    {
        void Save(RemindRequest request);
        void Delete(RemindRequest request);
        IEnumerable<RemindRequest> GetAll();
    }
}
