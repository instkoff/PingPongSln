using PingPong.Shared.Models.Dto;

namespace PingPong.Shared.Models.Responses
{
    public class MessageListResponse : BaseResponse
    {
        public MessageDto[] Messages { get; init; }
    }
}
