using System;

namespace PingPong.Shared.Models.Requests
{
    public class GetMessageRequest : BaseRequest
    {
        public Guid MessageId { get; set; }

    }
}
