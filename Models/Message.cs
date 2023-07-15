using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Message
    {
        [Key]
        public int Id {get; set;}
        public string Text {get; set;} = null!;
        public string? From {get; set;}
        public DateTime CreateDate {get; set;}
        public Group CreatingGroup {get;set;} = null!;
        public int GroupId {get; set;}
    }
}