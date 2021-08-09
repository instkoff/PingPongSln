using PingPong.Shared.Models.Dto;

namespace PingPong.Shared.Models.Responses
{
    public class MessageResponse : BaseResponse
    {
        public MessageDto Message { get; set; }
    }
}
