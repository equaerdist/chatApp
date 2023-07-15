namespace WebApplication5.Models
{
    public class UserSettingsForGroup
    {
        public int Id {get;set;}

        public bool ActiveNotifications {get;set;}
        public bool ActiveSound {get; set;}
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
    }
}