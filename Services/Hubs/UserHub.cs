using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebApplication5.Dto;
using WebApplication5.Models;
using WebApplication5.Services.Repository;

namespace WebApplication5.Services.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserHub : Hub
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        public override async Task OnConnectedAsync()
        {
            var userId = int.Parse(Context.User?.Claims.First(x => x.Type == "Id").Value);
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user is null)
                return;
            user.IsOnline = true;
            await _userRepository.SaveChangesAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = int.Parse(Context.User?.Claims.First(x => x.Type == "Id").Value);
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user is null)
                return;
            user.IsOnline = false;
            await _userRepository.SaveChangesAsync();
        }
        public UserHub(IMessageRepository messageRep, IGroupRepository groupRep, IMapper mapper, IUserRepository userRep) 
        {
            _mapper = mapper;
            _groupRepository = groupRep;
            _messageRepository = messageRep;
            _userRepository = userRep;
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
            var onlineUsersGroup = await _groupRepository.GetOnlineUsersForGroup(groupId);
            if (onlineUsersGroup != null) 
            {
                await _userRepository.GetUserByIdAsync(userId);
                var getMessageDto = _mapper.Map<GetMessageDto>(messageForRepo);
                await Clients.Users(onlineUsersGroup.Select(ug => ug.UserId.ToString())).SendAsync("Receive", getMessageDto);
            }
        }
    }
}
