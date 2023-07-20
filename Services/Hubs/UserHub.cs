using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebApplication5.Dto;
using WebApplication5.Models;
using WebApplication5.Services.Repository;

namespace WebApplication5.Services.Hubs
{
    [Authorize]
    public class UserHub : Hub
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IMessageRepository _messageRepository;

        public UserHub(IMessageRepository messageRep, IGroupRepository groupRep, IMapper mapper) 
        {
            _mapper = mapper;
            _groupRepository = groupRep;
            _messageRepository = messageRep;
        }
        public async Task SendToAll(AddMessageDto messageDto, int groupId)
        {
            var userId = int.Parse(Context.User?.Claims.First(x => x.Type == "Id").Value);
            if (!await _groupRepository.GroupContainsUser(userId, groupId))
                return;
            var messageForRepo = _mapper.Map<Message>(messageDto);
            messageForRepo.CreateDate = DateTime.UtcNow;
            messageForRepo.UserId = userId;
            messageForRepo.GroupId = groupId;
            await _messageRepository.AddMessageToTheGroupAsync(messageForRepo);
        }
    }
}
