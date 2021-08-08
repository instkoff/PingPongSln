using System;
using System.Threading.Tasks;
using PongApp.Domain.Models.Dto;
using PongApp.Domain.Models.Request;
using PongApp.Domain.Models.Responses;

namespace PongApp.Domain.Infrastructure.Interfaces.Services
{
    public interface IMessageService
    {
        Task<Guid> AddMessageAsync(MessageDto message);

        Task<MessageDto[]> GetMessageListAsync(string userName);

        Task<MessageDto> GetMessageAsync(MessageRequest request);

        Task<int> DeleteMessageAsync(MessageRequest messageRequest);
    }
}
