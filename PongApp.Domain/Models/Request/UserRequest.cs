using System;

namespace PongApp.Domain.Models.Request
{
    public class UserRequest
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
    }
}
