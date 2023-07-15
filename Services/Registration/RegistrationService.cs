using AutoMapper;
using WebApplication5.Dto;
using WebApplication5.Models;
using WebApplication5.Services.Passwords;
using WebApplication5.Services.Repository;

namespace WebApplication5.Services.Registration 
{
    public class RegistrationService : IRegistrationService
    {
        public static readonly string DEFAULT_THUMBNAIL = "user.png";
        private readonly IUserRepository _repository;
        private readonly IPasswordValidator _validator;
        private readonly IMapper _mapper;
        private readonly IPasswordHandler _handler;

        public RegistrationService(IUserRepository rep, IPasswordValidator validator, IMapper mapper, IPasswordHandler handler) 
        {
            _repository = rep;
            _validator = validator;
            _mapper = mapper;
            _handler = handler;
        }
        public async Task<RegistrationResult> RegisterUser(AddUserDto newUser)
        {
            var registrationResult = new RegistrationResult();
            newUser.Nickname = newUser.Nickname.ToLower();
            var similarUser = await _repository.GetUserByNickNameAsync(newUser.Nickname);
            if(similarUser is null) 
            {
                var passwordInfo = _validator.ValidatePassword(newUser);
                if(passwordInfo.EnumResult != PasswordValidationResult.Marks.Bad)
                {
                    var hashedPassword = _handler.GetHash(newUser.Password);
                    newUser.Password = hashedPassword;
                    registrationResult.PasswordInfo = passwordInfo;
                    var userForRepo = _mapper.Map<User>(newUser);
                    userForRepo.Thumbnail = DEFAULT_THUMBNAIL;
                    userForRepo.RegistrationDate = DateTime.UtcNow;
                    await _repository.AddUserAsync(userForRepo);
                    await _repository.SaveChangesAsync();
                    registrationResult.Result = "Регистрация прошла успешно";
                }
                else {
                    registrationResult.Result = "Ваш пароль недостаточно надежный";
                }
            }
            else {
                registrationResult.Result = "Такой пользователь уже существует";
            }
            return registrationResult;
        }
    }
}