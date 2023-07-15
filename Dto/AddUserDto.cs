using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Dto 
{
    public class AddUserDto 
    {
        [MinLength(5)]
        [Required, MaxLength(135)]
        public string FullName {get; set;} = null!;
        [MaxLength(100), Required]
        [MinLength(3)]
        public string Nickname {get;set; } = null!;
        [MinLength(8)]
        public string Password {get;set;} = null!;
    }
}