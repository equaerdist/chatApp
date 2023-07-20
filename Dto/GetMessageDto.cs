using WebApplication5.Models;

namespace WebApplication5.Dto
{
    public class GetMessageDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public GetUserDto Creator { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int GroupId { get; set; }   
    }
}
