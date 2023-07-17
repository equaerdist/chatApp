using WebApplication5.Models;

namespace WebApplication5.Dto
{
    public class AddMessageDto
    {
        public string Text { get; set; } = null!;
        public int GroupId { get; set; }
    }
}
