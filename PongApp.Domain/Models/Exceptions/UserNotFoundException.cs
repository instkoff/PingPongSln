using System;

namespace PongApp.Domain.Models.Exceptions
{
    /// <summary>
    /// Эксепшн если пользователь не найден.
    /// </summary>
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}