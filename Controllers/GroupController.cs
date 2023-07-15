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
        private readonly IUserRepository _userRepository;

        public GroupController(IGroupRepository rep, IMapper mapper, IUserRepository userRep)
        {
            _repository = rep;
            _mapper = mapper;
            _userRepository = userRep;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup(AddGroupDto newGroup)
        {
            var groupForRepo = _mapper.Map<Group>(newGroup);
            var creatorNickname = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var creator = await _userRepository.GetUserByNickNameAsync(creatorNickname);
            if (creator is not null)
            {
                groupForRepo.UsersGroup.Add(new UsersGroup() { Group = groupForRepo, IsAdmin = true, User = creator });
                await _repository.AddGroupAsync(groupForRepo);
                return NoContent();
            }
            return BadRequest();
        }
    }
}