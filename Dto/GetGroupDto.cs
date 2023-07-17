using System.ComponentModel.DataAnnotations;
using WebApplication5.Models;

namespace WebApplication5.Dto
{
    public class GetGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public string Thumbnail { get; set; } = null!;
        public GroupSettings Settings { get; set; } = null!;
    }
}
