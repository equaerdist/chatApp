using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Dto 
{
    public class AddUserDto 
    {
        [MinLength(5, ErrorMessage = "������ ��� �� ����� ���� ������ 5 ��������")]
        [Required(ErrorMessage = "���� �������� ������������"), MaxLength(135, ErrorMessage = "������������ ����� ��� ������� ����� �� ������ 135 ��������")]
        public string FullName {get; set;} = null!;
        [MaxLength(100, ErrorMessage = "��� �� ������ ��������� 100 ��������"), Required(ErrorMessage = "���� �������� ������������")]
        [MinLength(3, ErrorMessage = "��� �� ������ ���� ������ 3 ��������")]
        public string Nickname {get;set; } = null!;
        [MinLength(8, ErrorMessage = "������ �� ������ ���� ������ 8 ��������")]
        public string Password {get;set;} = null!;
    }
}