using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using WebApplication5.Dto;
using WebApplication5.Services.Registration;
using WebApplication5.Services.Repository;
using WebApplication5.Services.Repository.UserGroupsRepository;

namespace WebApplication5.Controllers 
{
  
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController : Controller 
    {
        private readonly IRegistrationService _registrationServce;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly IUserGroupsRepository _userGroupRepository;

        public UserController(
            IRegistrationService register, 
            IUserRepository rep, 
            IMapper mapper, 
            IUserGroupsRepository userGroup,
            ILogger<UserController> logger
            )
        {
            _registrationServce = register;
            _repository = rep;
            _mapper = mapper;
            _logger = logger;
            _userGroupRepository = userGroup;
        }
        [HttpPost]
        public async Task<IActionResult> AddUserAsync(AddUserDto newUser)
        {
                var result = await _registrationServce.RegisterUser(newUser);
                return Ok(result);
        }
        [Authorize]
        [HttpGet("{nickname:required}")]
        public async Task<IActionResult> GetUserAsync(string nickname)
        {
            if (User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value == nickname)
            {
                var userFromRepo = await _repository.GetUserByNickNameAsync(nickname);
                var userDto = _mapper.Map<GetUserDto>(userFromRepo);
                return Ok(userDto);
            }
            return Forbid();
                
        }
        [Authorize]
        [HttpGet("{id:int}/groups")]
        public async Task<IActionResult> GetGroupsForUser(int id, string? sortTerm, int pageSize, int page, string? sortOrder)
        {
            if (User.Claims.First(x => x.Type == "Id").Value != id.ToString()) return Forbid();
            return Ok(await _repository.GetGroupsForUserByIdAsync(id, sortTerm, page, pageSize, sortOrder));
        }
        [Authorize]
        [HttpGet("{id:int}/groups/{groupId:int}")]
        public async Task<IActionResult> GetGroupInfoForUser(int id, int groupId)
        {
            if (User.Claims.First(x => x.Type == "Id").Value != id.ToString()) return Forbid();
            var ug = await _userGroupRepository.GetAsync(id, groupId);
            return Ok(ug);
        }
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto newUser, int id)
        {
            
            if (User.Claims.First(x => x.Type == "Id").Value != id.ToString()) return Forbid();
            _logger.LogInformation("Заявка на обновление пользователя");
            var similarNickname = await _repository.GetUserByNickNameAsync(newUser.Nickname);
            _logger.LogInformation("Новое имя ни с кем непересекается, идем дальше");
            var needUser = await _repository.GetUserByIdAsync(id);
            if (needUser is null) return NotFound();
            if (similarNickname != null)
            {
                if (!needUser.Equals(similarNickname)) return BadRequest
                   (
                        new ValidationProblemDetails
                        {
                            Errors = { { "Пользователь", new[] { "Пользователь с таким никнеймом уже существует" } } },
                            Title = "Возникла ошибка при валидации",
                            Detail = "Смотрите подробнее в errors",
                            Status = (int)HttpStatusCode.BadRequest
                        }
                   );
            }
            _mapper.Map(newUser, needUser);
            await _repository.SaveChangesAsync();
            _logger.LogInformation($"пользователь обновлен");    
            return NoContent();
        }
    }
}