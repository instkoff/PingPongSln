using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingPong.Shared.Models.Responses;
using PongApp.Domain.Infrastructure.Interfaces.Services;
using PongApp.Domain.Models.Exceptions;
using PongApp.Domain.Models.Request;

namespace PongApp.Controllers
{
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly ILogger<MessageController> _logger;
        public MessageController(IMessageService messageService, ILogger<MessageController> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddMessageRequest messageRequest)
        {
            if (messageRequest == null)
            {
                _logger.LogError("Message model can't be null.");
                throw new ArgumentNullException(nameof(messageRequest), "Unexpected. Message request can't be null.");
            }

            var result = await _messageService.AddMessageAsync(messageRequest);

            if (result == Guid.Empty)
            {
                throw new AddMessageException("Unexpected. Add message failed.");
            }

            return Ok(new AddMessageResponse
            {
                Id = result,
                Status = 1
            });

        }

        [HttpPost("list")]
        public async Task<IActionResult> List(GetMessageRequest request)
        {
            if (request == null)
            {
                _logger.LogError("Request can't be null.");
                throw new ArgumentNullException(nameof(request), "Unexpected. Request can't be null.");
            }

            var messages = await _messageService.GetMessageListAsync(request);
            return Ok(new MessageListResponse
            {
                Messages = messages,
                Status = 1
            });
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(GetMessageRequest request)
        {
            if (request == null)
            {
                _logger.LogError("Request can't be null.");
                throw new ArgumentNullException(nameof(request), "Unexpected. Request can't be null.");
            }

            var result = await _messageService.DeleteMessageAsync(request);

            if (result == 0)
                throw new DeleteMessageException("Deleted messages count 0.");

            return Ok(new BaseResponse { Status = 1 });
        }
    }
}
