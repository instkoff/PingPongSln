using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PongApp.Domain.Infrastructure.Interfaces.Services;
using PongApp.Domain.Models.Dto;
using PongApp.Domain.Models.Exceptions;
using PongApp.Domain.Models.Request;
using PongApp.Domain.Models.Responses;

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
        public async Task<IActionResult> Add(MessageDto messageDto)
        {
            if (messageDto == null)
            {
                _logger.LogError("Message model can't be null.");
                throw new ArgumentNullException(nameof(messageDto), "Unexpected. Message model can't be null.");
            }

            var result = await _messageService.AddMessageAsync(messageDto);

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
        public async Task<IActionResult> List(MessageRequest request)
        {
            if (request == null)
            {
                _logger.LogError("Request can't be null.");
                throw new ArgumentNullException(nameof(request), "Unexpected. Request can't be null.");
            }

            if (request.MessageId == Guid.Empty)
            {
                return Ok(new MessageListResponse
                {
                    Messages = await _messageService.GetMessageListAsync(request.Username),
                    Status = 1
                });
            }

            return Ok(new MessageResponse 
            {
                Message = await _messageService.GetMessageAsync(request),
                Status = 1
            });
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(MessageRequest request)
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
