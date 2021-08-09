using System.Collections.Generic;

namespace PingPong.Shared.Models.Dto
{
    public class UserDto : BaseDto
    {
        public string Name { get; set; }

        public List<MessageDto> Messages { get; set; }
    }
}
