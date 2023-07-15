using WebApplication5.Dto;

namespace WebApplication5.Services.Passwords 
{
    public interface IPasswordValidator
    {
        PasswordValidationResult ValidatePassword(AddUserDto user);
    }
}