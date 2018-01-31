using System;

namespace RedditRemindCrypto.Business.Interpreters.Exceptions
{
    public class InvalidSyntaxException : Exception
    {
        public InvalidSyntaxException(string message)
            : base(message)
        {
        }
    }
}
