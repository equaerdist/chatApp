using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication5.Dto 
{
    public class AddUserDto 
    {
        [MinLength(5, ErrorMessage = "������ ��� �� ����� ���� ������ 5 ��������"), Display(Name = "������ ���")]
        [Required(ErrorMessage = "���� �������� ������������"), MaxLength(135, ErrorMessage = "������������ ����� ��� ������� ����� �� ������ 135 ��������")]
        public string FullName {get; set;} = null!;
        [MaxLength(100, ErrorMessage = "��� �� ������ ��������� 100 ��������"), Required(ErrorMessage = "���� �������� ������������")]
        [MinLength(3, ErrorMessage = "��� �� ������ ���� ������ 3 ��������"), Display(Name = "���������� ���")]
        public string Nickname {get;set; } = null!;
        [MinLength(8, ErrorMessage = "������ �� ������ ���� ������ 8 ��������")]
        [Display(Name = "������")]
        public string Password {get;set;} = null!;
    }
}