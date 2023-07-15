using WebApplication5.Services.Passwords;

namespace WebApplication5.Services.Registration 
{
    public class RegistrationResult 
    {
        public PasswordValidationResult? PasswordInfo {get; set;}
        public string? Result {get; set;} 
    }
}