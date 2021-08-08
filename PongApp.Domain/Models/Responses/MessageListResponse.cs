using PongApp.Domain.Models.Dto;

namespace PongApp.Domain.Models.Responses
{
    public class MessageListResponse : BaseResponse
    {
        public MessageDto[] Messages { get; init; }
    }
}
