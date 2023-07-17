using AutoMapper;
using WebApplication5.Dto;
using WebApplication5.Models;

namespace WebApplication5.AutoMapperProfiles 
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddUserDto, User>();
            CreateMap<User, GetUserDto>();
            CreateMap<ReceiveMessageDto, Message>();
            CreateMap<Message, SendMessageDto>();
            CreateMap<Group, GetGroupDto>();
            CreateMap<AddMessageDto, Message>();
            CreateMap<Message, GetMessageDto>();
        }
    }
}