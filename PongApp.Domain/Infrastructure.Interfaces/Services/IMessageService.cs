using System;
using System.Threading.Tasks;
using PingPong.Shared.Models.Dto;
using PingPong.Shared.Models.Requests;

namespace PongApp.Domain.Infrastructure.Interfaces.Services
{
    /// <summary>
    /// Интерфейс сервиса сообщений
    /// </summary>
    public interface IMessageService
    {
        Task<Guid> AddMessageAsync(AddMessageRequest message);

        Task<MessageDto[]> GetMessageListAsync(GetMessageRequest request);

        Task<int> DeleteMessageAsync(GetMessageRequest getMessageRequest);
    }
}