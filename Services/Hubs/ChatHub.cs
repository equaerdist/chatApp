using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebApplication5.Dto;
using WebApplication5.Models;
using WebApplication5.Services.Repository;

namespace WebApplication5.Services.Hubs
{
    [Authorize]
    class ChatHub : Hub
    {
        private readonly IGroupRepository _repository;
       
        private readonly IMapper _mapper;

        public ChatHub(IGroupRepository rep, IMapper mapper) 
        {
            _repository = rep;
            _mapper = mapper;
        }
        public async Task SendMessage(ReceiveMessageDto message)
        {
            var group = await _repository.GetGroupByIdAsync(message.GroupId);
            var userIds = group?.Users.Select(u => u.Nickname);
            var messageForRepo = _mapper.Map<Message>(message);
            messageForRepo.CreateDate = DateTime.UtcNow;
            group?.Messages.Add(messageForRepo);
            await _repository.SaveChangesAsync();
            var sendMessage = _mapper.Map<SendMessageDto>(messageForRepo);
            if(userIds is not null)
                await Clients.Users(userIds).SendAsync("Receive", sendMessage);
        }
    }
 }