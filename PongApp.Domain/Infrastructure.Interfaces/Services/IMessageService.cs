using System;
using System.Threading.Tasks;
using PingPong.Shared.Models.Dto;
using PongApp.Domain.Models.Request;

namespace PongApp.Domain.Infrastructure.Interfaces.Services
{
    public interface IMessageService
    {
        Task<Guid> AddMessageAsync(AddMessageRequest message);

        Task<MessageDto[]> GetMessageListAsync(GetMessageRequest request);

        Task<int> DeleteMessageAsync(GetMessageRequest getMessageRequest);
    }
}