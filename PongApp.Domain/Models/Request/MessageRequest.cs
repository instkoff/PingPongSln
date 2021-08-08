using System;

namespace PongApp.Domain.Models.Request
{
    public class MessageRequest
    {
        public Guid MessageId { get; set; }

        public string Username { get; set; }
    }
}
