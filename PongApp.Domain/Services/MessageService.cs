using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PongApp.DataAccess.Entities;
using PongApp.DataAccess.Infrastructure.Interfaces;
using PongApp.Domain.Infrastructure.Interfaces.Services;
using PongApp.Domain.Models.Dto;
using PongApp.Domain.Models.Exceptions;
using PongApp.Domain.Models.Request;

namespace PongApp.Domain.Services
{
    public class MessageService : IMessageService
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public MessageService(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> AddMessageAsync(MessageDto message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message), "Unexpected. Message request is null.");

            var userEntity = _dbContext.Users.FirstOrDefault(x=>x.Name == message.UserName);
            var messageEntity = _mapper.Map<MessageEntity>(message);

            if (userEntity != null)
            {
                messageEntity.UserId = userEntity.Id;
            }
            else
            {
                messageEntity.User = new UserEntity { Name = message.UserName };
            }

            var addResult = await _dbContext.Messages.AddAsync(messageEntity);
            await _dbContext.SaveChangesAsync();

            return addResult.Entity.Id;
        }

        public async Task<MessageDto[]> GetMessageListAsync(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentException("Unexpected. Username is empty.", nameof(username));

            var userEntity = await _dbContext.Users
                .Include(u => u.Messages)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == username);

            if (userEntity == null)
            {
                throw new UserNotFoundException($"User with name {username} not found.");
            }

            return _mapper.Map<MessageDto[]>(userEntity.Messages);
        }

        public async Task<MessageDto> GetMessageAsync(MessageRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Unexpected. Message request is null.");

            var messageEntity = await _dbContext.Messages
                .Include(m => m.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.User.Name == request.Username && x.Id == request.MessageId);

            if (messageEntity == null)
            {
                throw new MessageNotFoundException($"Message not found.\n MessageId: {request.MessageId}\n Username: {request.Username}");
            }

            return _mapper.Map<MessageDto>(messageEntity);
        }

        public async Task<int> DeleteMessageAsync(MessageRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Unexpected. Message request is null.");

            var messageEntity = await _dbContext.Messages
                .Include(m => m.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.User.Name == request.Username && x.Id == request.MessageId);

            if (messageEntity == null)
            {
                throw new MessageNotFoundException($"Message not found.\n MessageId: {request.MessageId}\n Username: {request.Username}");
            }

            _dbContext.Messages.Remove(messageEntity);

             return await _dbContext.SaveChangesAsync();
        }
    }
}
