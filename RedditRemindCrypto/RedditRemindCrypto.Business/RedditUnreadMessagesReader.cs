using RedditRemindCrypto.Business.Expressions;
using RedditRemindCrypto.Business.Factories;
using RedditRemindCrypto.Business.Services;
using RedditRemindCrypto.Business.Services.Models;
using RedditRemindCrypto.Business.Settings;
using RedditSharp;
using RedditSharp.Things;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace RedditRemindCrypto.Business
{
    public class RedditUnreadMessagesReader
    {
        private readonly Reddit client;
        private readonly ExpressionExtractor expressionExtractor;
        private readonly IRemindRequestService remindRequestService;

        public RedditUnreadMessagesReader(IBotSettings botSettings, IRemindRequestService remindRequestService, ExpressionExtractor expressionExtractor, RedditClientFactory clientFactory)
        {
            this.client = clientFactory.Create(botSettings);
            this.expressionExtractor = expressionExtractor;
            this.remindRequestService = remindRequestService;
        }

        public void ReadUnreadComments()
        {
            try
            {
                foreach (var item in client.User.UnreadMessages.Where(x => x.Kind == "t1").Take(20))
                {
                    var comment = item as Comment;
                    if (comment == null)
                        continue;

                    Handle(comment);
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
            }
        }

        public void ReadUnreadPrivateMessages()
        {
            foreach (var item in client.User.UnreadMessages.Where(x => x.Kind == "t4").Take(20))
            {
                var privateMessage = item as PrivateMessage;
                if (privateMessage == null)
                    continue;

                Handle(privateMessage);
            }
        }

        private void Handle(Comment comment)
        {
            try
            {
                comment.SetAsRead();

                if (!ContainsUserMention(comment))
                    return;

                var remindRequests = new List<RemindRequest>();
                var decodedBody = WebUtility.HtmlDecode(comment.Body);
                var extractionResult = expressionExtractor.Extract(decodedBody);
                if (!extractionResult.Expressions.Any() && !extractionResult.InvalidExpressions.Any())
                    return;

                foreach (var expression in extractionResult.Expressions)
                {
                    var request = CreateRemindRequest(comment, expression);
                    remindRequestService.Save(request);
                    remindRequests.Add(request);
                }

                var message = CreateReplyMessage(remindRequests, extractionResult.InvalidExpressions);
                if (!string.IsNullOrEmpty(message))
                    comment.Reply(message);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
            }
        }

        private void Handle(PrivateMessage privateMessage)
        {
            try
            {
                privateMessage.SetAsRead();

                if (privateMessage.Body == "-list")
                {
                    // pm all reminders to user (privateMessage.Author?)
                }
                else if (privateMessage.Body.StartsWith("-delete"))
                {
                    // get guid from body
                    // delete reminder if user of id matches user of pm
                }
            }
            catch (Exception e)
            {
            }
        }

        private bool ContainsUserMention(Comment comment)
        {
            return comment.Body.Contains("/u/RemindCryptoBot");
        }

        private RemindRequest CreateRemindRequest(Comment comment, string expression)
        {
            // Shortlink starts with oath, which it shouldnt, so we build it the dirty way ourselves.
            var permalink = $"/r/{comment.Subreddit}/comments/{comment.LinkId}/_/{comment.Id}";
            return new RemindRequest
            {
                Expression = expression,
                User = comment.AuthorName,
                Permalink = permalink
            };
        }

        private string CreateReplyMessage(IEnumerable<RemindRequest> createdRequests, IEnumerable<string> invalidExpressions)
        {
            // Reddit handles a double linebreaks as a linebreak, therefore after each AppendLine() we add another empty AppendLine()
            var builder = new StringBuilder();
            if (createdRequests.Any())
            {
                builder.AppendLine($"A private message will be send to you when your condition is met:");
                builder.AppendLine();
                foreach (var request in createdRequests)
                {
                    builder.AppendLine(request.Expression);
                    builder.AppendLine();
                }
            }
            if (invalidExpressions.Any())
            {
                builder.AppendLine("Unable to evaluate the following expressions");
                builder.AppendLine();
                foreach (var expression in invalidExpressions)
                {
                    builder.AppendLine(expression);
                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }
    }
}
