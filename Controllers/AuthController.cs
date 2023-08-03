using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication5.Dto;
using WebApplication5.Services.Passwords;
using WebApplication5.Services.Repository;

namespace WebApplication5.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller 
    {
        private readonly string ERROR_MESSAGE = "Проверьте корректность введенных данных";
        private IUserRepository _repository;
        private readonly AppOptions _options;
        private readonly IPasswordHandler _handler;

        public AuthController(IUserRepository rep, AppOptions options, IPasswordHandler handler)
        {
            _repository = rep;
            _options = options;
            _handler = handler;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CheckAuth()
        {
            await Task.Delay(1);
            var user = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(new {nickname = user});
        }

        [HttpPost]
        public async Task<IActionResult> GetAuth(GetAuthDto inputData)
        {
                var user = await _repository.GetUserByNickNameAsync(inputData.Nickname);
                if (user is null || !_handler.Verify(inputData.Password, user.Password)) 
                    return BadRequest(
                        new ValidationProblemDetails(new Dictionary<string, string[]>() { { "Данные", new[] {ERROR_MESSAGE}}})
                            {
                                Title = ERROR_MESSAGE,
                                Status = (int)HttpStatusCode.BadRequest,
                                Detail = "Read more in errors"
                            }
                        );
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Nickname),
                    new("Thumbnail", user.Thumbnail),
                    new("Id", user.Id.ToString())
                };
                var token = new JwtSecurityToken
                (
                    claims: claims, 
                    expires: DateTime.UtcNow.Add(TimeSpan.FromHours(10)), 
                    signingCredentials: new SigningCredentials(_options.Key, SecurityAlgorithms.HmacSha256)
                );
                var securityToken = new JwtSecurityTokenHandler();
                return Ok(new { token = securityToken.WriteToken(token) });
        }
    }
}