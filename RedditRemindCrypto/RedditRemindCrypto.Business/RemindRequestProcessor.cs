using RedditRemindCrypto.Business.Services;

namespace RedditRemindCrypto.Business
{
    public class RemindRequestProcessor
    {
        private readonly RemindRequestHandler handler;
        private readonly IRemindRequestService service;

        public RemindRequestProcessor(RemindRequestHandler handler, IRemindRequestService service)
        {
            this.handler = handler;
            this.service = service;
        }

        public void Process()
        {
            var requests = service.GetAll();
            handler.Handle(requests);
        }
    }
}
