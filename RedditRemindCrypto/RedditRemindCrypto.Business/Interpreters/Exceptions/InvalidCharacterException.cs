using System;

namespace RedditRemindCrypto.Business.Interpreters.Exceptions
{
    public class InvalidCharacterException : Exception
    {
        public InvalidCharacterException(string message)
            : base(message)
        {
        }
    }
}
