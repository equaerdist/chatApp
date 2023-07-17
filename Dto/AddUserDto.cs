using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication5.Dto 
{
    public class AddUserDto 
    {
        [MinLength(5, ErrorMessage = "Полное имя не может быть короче 5 символов"), Display(Name = "Полное имя")]
        [Required(ErrorMessage = "Поле является обязательным"), MaxLength(135, ErrorMessage = "Максимальная длина для полного имени не больше 135 символов")]
        public string FullName {get; set;} = null!;
        [MaxLength(100, ErrorMessage = "Ник не должен превышать 100 символов"), Required(ErrorMessage = "Поле является обязательным")]
        [MinLength(3, ErrorMessage = "Ник не должен быть короче 3 символов"), Display(Name = "Уникальное имя")]
        public string Nickname {get;set; } = null!;
        [MinLength(8, ErrorMessage = "Пароль не должен быть короче 8 символов")]
        [Display(Name = "Пароль")]
        public string Password {get;set;} = null!;
    }
}