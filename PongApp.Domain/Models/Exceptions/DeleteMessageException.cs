using System;

namespace PongApp.Domain.Models.Exceptions
{
    public class DeleteMessageException : Exception
    {
        public DeleteMessageException(string message) : base(message)
        {
            
        }
    }
}
