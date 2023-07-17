using System.Text.RegularExpressions;

namespace WebApplication5.Dto 
{
    public class GetUserDto 
    {
        public int Id { get; set; }
        public string FullName {get; set;} = null!;
        public string Nickname {get;set; } = null!;
        public DateTime RegistrationDate {get;set;}
        public string Thumbnail {get; set;} = null!;
    }
}