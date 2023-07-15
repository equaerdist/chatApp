using System.Text.RegularExpressions;
using WebApplication5.Dto;

namespace WebApplication5.Services.Passwords 
{
    public class DefaultPasswordValidator : IPasswordValidator
    {
        private float _trimScore(float score)
        {
            return score > 1 ? 1 : score;
        }
        public static readonly int ENOUGH_DIGIT = 5;
        private bool checkLowerAndUpperCase(string password)
        {
           return password.Any(char.IsLower) && password.Any(char.IsUpper);
        }
        public PasswordValidationResult ValidatePassword(AddUserDto user)
        {
           var result = new PasswordValidationResult();
           string password = user.Password;
           var score = 0.2f;
           if(checkLowerAndUpperCase(password)) score += 0.1f;
            score += ratePasswordDigits(password);
            if(checkSensitiveInfo(user)) score += 0.5f;
            result.Result = _trimScore(score);
            return result;
        }
        private bool checkSensitiveInfo(AddUserDto user)
        {
            var fullName = user.FullName.ToLower().Replace(Char.ToString(' '), string.Empty);
            var nickName = user.Nickname.ToLower();
            var passwordLower = user.Password.ToLower();
            return passwordLower.IndexOf(fullName) == -1 && passwordLower.IndexOf(nickName) == -1;
        }
        private float ratePasswordDigits(string password)
        {
            var regex = new Regex(@"\d");
            var digits = regex.Matches(password);
            var result = digits.Count() / ENOUGH_DIGIT;
            return result > 0.3 ? 0.5f : result;
        }
    }
}