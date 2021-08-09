using System;

namespace PongApp.Domain.Models.Exceptions
{
    public class AddMessageException : Exception
    {
        public AddMessageException(string message) : base(message)
        {
        }
    }
}