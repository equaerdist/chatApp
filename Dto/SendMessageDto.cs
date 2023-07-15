

using WebApplication5.Models;

namespace WebApplication5.Dto
{
    public class SendMessageDto
    {
        public string Text {get; set;} = null!;
        public string? From {get; set;}
        public DateTime CreateDate {get; set;}
        public Group CreatingGroup {get;set;} = null!;
    }
}