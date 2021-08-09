using System.Collections.Generic;

namespace PongApp.DataAccess.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<MessageEntity> Messages { get; set; }
    }
}