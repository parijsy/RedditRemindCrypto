using RedditRemindCrypto.Business.Expressions;
using RedditRemindCrypto.Business.Factories;
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
        private readonly ExpressionReader expressionReader;
        private readonly ExpressionEvaluator expressionEvaluator;
        private readonly IRemindRequestService remindRequestService;
        private readonly Reddit client;

        public RemindRequestHandler(IBotSettings botSettings, IRemindRequestService remindRequestService, ExpressionReader expressionReader, ExpressionEvaluator expressionEvaluator, RedditClientFactory clientFactory)
        {
            this.client = clientFactory.Create(botSettings);
            this.expressionReader = expressionReader;
            this.expressionEvaluator = expressionEvaluator;
            this.remindRequestService = remindRequestService;
        }

        public void Handle(IEnumerable<RemindRequest> reminders)
        {
            foreach (var reminder in reminders)
                Handle(reminder);
        }

        private void Handle(RemindRequest request)
        {
            var expression = expressionReader.Read(request.Expression);
            var conditionMet = expressionEvaluator.Evaluate(expression);
            if (!conditionMet)
                return;

            SendReminderAndDeleteRequest(request);
        }

        private void SendReminderAndDeleteRequest(RemindRequest request)
        {
            var reminder = CreateReminder(request);
            client.ComposePrivateMessage(reminder.Subject, reminder.Body, reminder.User);
            remindRequestService.Delete(request);
        }

        private Reminder CreateReminder(RemindRequest request)
        {
            return new Reminder
            {
                Subject = "RemindCrypto Reminder",
                Body = CreateReminderBody(request),
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

        private class Reminder
        {
            public string Subject { get; set; }
            public string Body { get; set; }
            public string User { get; set; }
        }
    }
}
