using WebApplication5.Dto;
using WebApplication5.Models;

namespace WebApplication5.Services.Registration 
{
    public interface IRegistrationService
    {
        Task<RegistrationResult> RegisterUser(AddUserDto newUser);
    }
}