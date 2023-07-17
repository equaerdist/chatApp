using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication5.Dto;
using WebApplication5.Models;
using WebApplication5.Services.Repository;

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

        public GroupController(IGroupRepository rep, IMapper mapper, IMessageRepository messageRep)
        {
            _repository = rep;
            _mapper = mapper;
            _messageRepository = messageRep;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup(AddGroupDto newGroup)
        {
            var groupForRepo = _mapper.Map<Group>(newGroup);
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
        [HttpGet("messages/{id:int}")]
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
        [HttpPost("{messages}")]
        public async Task<IActionResult> AddMessageToGroup(AddMessageDto message)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "Id").Value);
            if (await _repository.GroupContainsUser(userId, message.GroupId))
            {
                var messageForRepo = _mapper.Map<Message>(message);
                messageForRepo.UserId = userId;
                messageForRepo.CreateDate = DateTime.UtcNow;
                await _messageRepository.AddMessageToTheGroupAsync(messageForRepo);
                await _messageRepository.SaveChangesAsync();
                return NoContent();
            }
            return Forbid(); 
        }
    }
    
}