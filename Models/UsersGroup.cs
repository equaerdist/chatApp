using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class UsersGroup {
        [Key]
        public int Id {get; set;}
        public User User { get; set; } = null!;
        public Group Group { get; set; } = null!;
        public bool IsAdmin {get; set;} = false;
    }
}