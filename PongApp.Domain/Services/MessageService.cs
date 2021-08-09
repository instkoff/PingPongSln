using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PingPong.Shared.Models.Dto;
using PongApp.DataAccess.Entities;
using PongApp.DataAccess.Infrastructure.Interfaces;
using PongApp.Domain.Infrastructure.Interfaces.Services;
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

        public async Task<Guid> AddMessageAsync(AddMessageRequest messageRequest)
        {
            if (messageRequest == null) throw new ArgumentNullException(nameof(messageRequest), "Unexpected. Message request is null.");
            if (string.IsNullOrEmpty(messageRequest.User)) throw new ArgumentException("Unexpected. Username is empty.", nameof(messageRequest.User));

            var userEntity = _dbContext.Users.FirstOrDefault(x=>x.Name == messageRequest.User);
            var messageEntity = _mapper.Map<MessageEntity>(messageRequest);

            if (userEntity != null)
            {
                messageEntity.UserId = userEntity.Id;
            }
            else
            {
                messageEntity.User = new UserEntity { Name = messageRequest.User };
            }

            var addResult = await _dbContext.Messages.AddAsync(messageEntity);
            await _dbContext.SaveChangesAsync();

            return addResult.Entity.Id;
        }

        public async Task<MessageDto[]> GetMessageListAsync(GetMessageRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Unexpected. Message request is null.");

            var query = _dbContext.Messages
                .Include(m => m.User)
                .Where(x => x.User.Name == request.User);

            if (request.MessageId != Guid.Empty)
            {
                query = query.Where(x => x.Id == request.MessageId);
            }

            var result = await query.ToArrayAsync();
            return _mapper.Map<MessageDto[]>(result);
        }

        public async Task<int> DeleteMessageAsync(GetMessageRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Unexpected. Message request is null.");

            var messageEntity = await _dbContext.Messages
                .Include(m => m.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.User.Name == request.User && x.Id == request.MessageId);

            if (messageEntity == null)
            {
                throw new MessageNotFoundException($"Message not found.\n MessageId: {request.MessageId}\n Username: {request.User}");
            }

            _dbContext.Messages.Remove(messageEntity);

             return await _dbContext.SaveChangesAsync();
        }
    }
}
