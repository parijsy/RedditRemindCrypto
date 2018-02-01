using RedditRemindCrypto.Business.Factories;
using RedditRemindCrypto.Business.Interpreters;
using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.Services.Models;
using RedditRemindCrypto.Business.Settings;
using RedditSharp;
using System.Collections.Generic;
using System.Text;

namespace RedditRemindCrypto.Business
{
    public class RemindRequestHandler
    {
        private readonly IRemindRequestService remindRequestService;
        private readonly InterpreterFactory interpreterFactory;
        private readonly Reddit client;

        public RemindRequestHandler(IBotSettings botSettings, IRemindRequestService remindRequestService, InterpreterFactory interpreterFactory, RedditClientFactory clientFactory)
        {
            this.client = clientFactory.Create(botSettings);
            this.remindRequestService = remindRequestService;
            this.interpreterFactory = interpreterFactory;
        }

        public void Handle(IEnumerable<RemindRequest> reminders)
        {
            foreach (var reminder in reminders)
                Handle(reminder);
        }

        private void Handle(RemindRequest request)
        {
            var interpreter = interpreterFactory.Create(request.Expression);
            var interpreterResult = interpreter.Interpret();
            if (interpreterResult.IsAlwaysFalse.HasValue && interpreterResult.IsAlwaysFalse.Value)
                SendAlwaysFalseMessageAndDeleteRequest(request);
            else if (interpreterResult.Result)
                SendReminderAndDeleteRequest(request);
        }

        private void SendReminderAndDeleteRequest(RemindRequest request)
        {
            var reminder = CreateReminder(request);
            client.ComposePrivateMessage(reminder.Subject, reminder.Body, reminder.User);
            remindRequestService.Delete(request);
        }

        private void SendAlwaysFalseMessageAndDeleteRequest(RemindRequest request)
        {
            var message = CreateAlwaysFalseMessage(request);
            client.ComposePrivateMessage(message.Subject, message.Body, message.User);
            remindRequestService.Delete(request);
        }

        private PrivateMessage CreateReminder(RemindRequest request)
        {
            return new PrivateMessage
            {
                Subject = "RemindCrypto Reminder",
                Body = CreateReminderBody(request),
                User = request.User
            };
        }

        private PrivateMessage CreateAlwaysFalseMessage(RemindRequest request)
        {
            return new PrivateMessage
            {
                Subject = "RemindCrypto Reminder",
                Body = CreateAlwaysFalseMessageBody(request),
                User = request.User
            };
        }

        private string CreateReminderBody(RemindRequest request)
        {
            var bodyBuilder = new StringBuilder($"The condition '{request.Expression}' has been met.");
            if (!string.IsNullOrEmpty(request.Permalink))
            {
                bodyBuilder.AppendLine();
                bodyBuilder.AppendLine($"[Go to comment]({request.Permalink})");
            }
            return bodyBuilder.ToString();
        }

        private string CreateAlwaysFalseMessageBody(RemindRequest request)
        {
            var bodyBuilder = new StringBuilder($"It seems that the condition '{request.Expression}' can never be met.");
            if (!string.IsNullOrEmpty(request.Permalink))
            {
                bodyBuilder.AppendLine();
                bodyBuilder.AppendLine($"[Go to comment]({request.Permalink})");
            }
            return bodyBuilder.ToString();
        }

        private class PrivateMessage
        {
            public string Subject { get; set; }
            public string Body { get; set; }
            public string User { get; set; }
        }
    }
}
