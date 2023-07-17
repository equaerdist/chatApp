using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using WebApplication5.Dto;
using WebApplication5.Services.Registration;
using WebApplication5.Services.Repository;

namespace WebApplication5.Controllers 
{
  
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController : Controller 
    {
        private readonly IRegistrationService _registrationServce;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserController(IRegistrationService register, IUserRepository rep, IMapper mapper)
        {
            _registrationServce = register;
            _repository = rep;
            _mapper = mapper;
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
    }
}