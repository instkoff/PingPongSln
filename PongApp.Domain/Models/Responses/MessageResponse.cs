using PongApp.Domain.Models.Dto;

namespace PongApp.Domain.Models.Responses
{
    public class MessageResponse : BaseResponse
    {
        public MessageDto Message { get; set; }
    }
}
