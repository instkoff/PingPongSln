using System;

namespace PongApp.Domain.Models.Exceptions
{
    /// <summary>
    /// Эксепшн если ошибка при удалении сообщения
    /// </summary>
    public class DeleteMessageException : Exception
    {
        public DeleteMessageException(string message) : base(message)
        {
        }
    }
}