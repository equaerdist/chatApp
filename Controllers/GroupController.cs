using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WebApplication5.Dto;
using WebApplication5.Exceptions;
using WebApplication5.Models;
using WebApplication5.Services.GroupManager;
using WebApplication5.Services.Repository;
using WebApplication5.Services.Repository.UserGroupsRepository;

namespace WebApplication5.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserGroupsRepository _userGroupsRepository;
        private readonly IGroupManager _manager;

        public GroupController(IGroupRepository rep, IMapper mapper, IMessageRepository messageRep, IUserGroupsRepository userGroupsRep, IGroupManager manager)
        {
            _repository = rep;
            _mapper = mapper;
            _messageRepository = messageRep;
            _userGroupsRepository = userGroupsRep;
            _manager = manager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup(AddGroupDto newGroup)
        {
            var groupForRepo = _mapper.Map<Group>(newGroup);
            if (groupForRepo.Thumbnail is null) groupForRepo.Thumbnail = "user.png";
            var creatorId = int.Parse(User.Claims.First(x => x.Type == "Id").Value);
            groupForRepo.UsersGroup.Add(new UsersGroup() { Group = groupForRepo, IsAdmin = true, UserId = creatorId });
            await _repository.AddGroupAsync(groupForRepo);
            return NoContent();
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGroupByIdAync(int id)
        {
            var group = await _repository.GetGroupByIdAsync(id);
            return Ok(_mapper.Map<GetGroupDto>(group));
        }
        [HttpGet("{id:int}/messages")]
        public async Task<IActionResult> GetMessagesForGroupById(int id, int pageSize, int page)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "Id").Value);
            if (await _repository.GroupContainsUser(userId, id))
            {
                var messages = await _repository.GetMessagesForGroupByIdAsync(id, pageSize, page);
                var responseMessages = _mapper.Map<IEnumerable<GetMessageDto>>(messages);
                return Ok(responseMessages);
            }
            return Forbid();
        }
        [HttpPost("{id:int}/messages")]
        public async Task<IActionResult> AddMessageToGroup(AddMessageDto message, int id)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "Id").Value);
            if (await _repository.GroupContainsUser(userId, id))
            {
                var messageForRepo = _mapper.Map<Message>(message); 
                messageForRepo.UserId = userId;
                messageForRepo.CreateDate = DateTime.UtcNow;
                messageForRepo.GroupId = id;
                await _messageRepository.AddMessageToTheGroupAsync(messageForRepo);
                await _messageRepository.SaveChangesAsync();
                var responseMessage = _mapper.Map<GetMessageDto>(messageForRepo);
                return Ok(responseMessage);
            }
            return Forbid(); 
        }
        [HttpGet]
        public async Task<IActionResult> GetGroupsBySearchTerm(string searchTerm)
        {
            var groups = await _repository.GetGroupBySearchTermAsync(searchTerm);
            return Ok(groups);
        }
        [HttpGet("{groupId:int}/manage")]
        public async Task<IActionResult> ManageUserToTheGroup(int groupId, string action)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "Id").Value);
            try
            {
                if (string.IsNullOrEmpty(action) || action == "enter")
                    await _manager.AddUserToGroup(groupId, userId);
                else
                    await _manager.DeleteUserFromGroup(groupId, userId);
                return NoContent();
            }
            catch (GroupManagerException ex)
            {
                return BadRequest(
                    new ValidationProblemDetails(new Dictionary<string, string[]> { { "Группа", new[] { ex.Message } } })
                    { Detail = "Просмотрите errors для пдоробностей", Title = "Ошибка при проверке логических данных" });
            }
            
        }
    }
    
}