using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using PingPong.Shared.Models.Dto;
using PongApp.DataAccess.Entities;

namespace PongApp.Domain.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //В ДТО
            CreateMap<UserEntity, UserDto>();
            CreateMap<MessageEntity, MessageDto>().ForMember(dest => dest.UserName, opt=>opt.MapFrom(src=>src.User == null ? string.Empty : src.User.Name));

            //В Entity
            CreateMap<UserDto, UserEntity>();
            CreateMap<MessageDto, MessageEntity>().EqualityComparison((dto, e) => dto.Id == e.Id);
        }
    }
}
