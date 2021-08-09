using System;

namespace PongApp.Domain.Models.Exceptions
{
    /// <summary>
    /// Эксепшн если сообщение не найдено
    /// </summary>
    public class MessageNotFoundException : Exception
    {
        public MessageNotFoundException(string message) : base(message)
        {
        }
    }
}