using System;

namespace PongApp.Domain.Models.Request
{
    public class GetMessageRequest : BaseRequest
    {
        public Guid MessageId { get; set; }
    }
}