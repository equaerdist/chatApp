using WebApplication5.Models;

namespace WebApplication5.Dto
{
    public class GetGroupDto
    {
       
        public int Id { get; set; }
       
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime RegistrationDate {get;set;}
        public string Thumbnail {get;set;} = null!;
        public ICollection<UsersGroup> UsersGroup {get;set;} = new List<UsersGroup>();
        public ICollection<Message> Messages {get; set;} = new List<Message>();
        public GroupSettings Settings {get; set;} = null!;
        public ICollection<UserSettingsForGroup> UserSettings {get;set;} = new List<UserSettingsForGroup>();
        public ICollection<User> Users { get => UsersGroup.Select(u => u.User).ToList();}
        public ICollection<User> Admins {get => UsersGroup.Where(u => u.IsAdmin).Select(u => u.User).ToList();}
    }
}