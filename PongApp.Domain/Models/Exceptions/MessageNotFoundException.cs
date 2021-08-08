using System;

namespace PongApp.Domain.Models.Exceptions
{
    public class MessageNotFoundException : Exception
    {
        public MessageNotFoundException(string message) : base(message)
        {
            
        }
    }
}
