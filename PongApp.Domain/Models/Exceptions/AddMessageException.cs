using System;

namespace PongApp.Domain.Models.Exceptions
{
    /// <summary>
    /// Эксепшн при ошибке добавления сообщения
    /// </summary>
    public class AddMessageException : Exception
    {
        public AddMessageException(string message) : base(message)
        {
        }
    }
}