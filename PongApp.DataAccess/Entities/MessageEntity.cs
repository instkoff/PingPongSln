using System;

namespace PongApp.DataAccess.Entities
{
    public class MessageEntity : BaseEntity
    {
        public string MessageText { get; set; }

        public Guid UserId { get; set; }

        public UserEntity User {get; set; }
    }
}
