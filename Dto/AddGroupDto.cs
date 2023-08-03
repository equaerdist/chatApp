using System.ComponentModel.DataAnnotations;
using WebApplication5.Models;

namespace WebApplication5.Dto
{
    public class AddGroupDto
    {
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Thumbnail {get;set;}
        public GroupSettings Settings {get; set;} = new GroupSettings() { IsPrivate = false, MaxUsersAmount = 0, MessagesOnlyForAdmins = false};
    }
}