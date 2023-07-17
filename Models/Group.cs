using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public string Thumbnail { get; set; } = null!;
        public ICollection<UsersGroup> UsersGroup { get; set; } = new List<UsersGroup>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public GroupSettings Settings { get; set; } = null!;
        public ICollection<UserSettingsForGroup> UserSettings { get; set; } = new List<UserSettingsForGroup>();
        [NotMapped]
        public ICollection<User> Users { get => UsersGroup.Where(ug => ug.IsAdmin is false).Select(ug => ug.User).ToList(); }
        [NotMapped]
        public ICollection<User> Admins { get => UsersGroup.Where(u => u.IsAdmin).Select(u => u.User).ToList(); }
        public DateTime LastMessageTime { get; set; }
        [NotMapped]
        public IEnumerable<Message> LastMessage
        {
            get => Messages.TakeLast(1);
        }
    }
}