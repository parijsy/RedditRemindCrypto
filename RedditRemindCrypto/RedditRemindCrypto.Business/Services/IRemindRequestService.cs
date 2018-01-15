using RedditRemindCrypto.Business.Services.Models;
using System;
using System.Collections.Generic;

namespace RedditRemindCrypto.Business.Services
{
    public interface IRemindRequestService
    {
        void Save(RemindRequest request);
        void Delete(RemindRequest request);
        void DeleteByUserAndId(string user, Guid id);
        IEnumerable<RemindRequest> GetAll();
        IEnumerable<RemindRequest> GetByUser(string user);
    }
}
