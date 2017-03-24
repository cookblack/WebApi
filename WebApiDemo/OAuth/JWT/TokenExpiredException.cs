using System;

namespace Cook.WebApi.OAuth.JWT
{
    public class TokenExpiredException : Exception
    {
        public TokenExpiredException(string message)
            : base(message)
        {
        }
    }
}