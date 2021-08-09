namespace PongApp.Domain.Models.Request
{
    public class AddMessageRequest : BaseRequest
    {
        public string Message { get; set; }
    }
}