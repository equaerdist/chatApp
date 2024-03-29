using System.ComponentModel.DataAnnotations;
using System.Data;
namespace WebApplication5.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string FullName {get; set;} = null!;
        [MaxLength(100)]
        public string Nickname {get;set; } = null!;
        public DateTime RegistrationDate {get;set;}
        public string Password {get;set;} = null!;
        public string Thumbnail {get; set;} = null!;
        public bool IsOnline { get; set;}
        public ICollection<UsersGroup> UserGroups {get; set;} = new List<UsersGroup>();
        public ICollection<UserSettingsForGroup> GroupSettings {get; set;} = new List<UserSettingsForGroup>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public override bool Equals(object? obj)
        {
            if (obj is not User) throw new ArgumentException();
            var user = (User)obj;
            return (user.FullName == FullName && user.Nickname == Nickname);
        }
    }
}