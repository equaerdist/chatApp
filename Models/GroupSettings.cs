using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Models
{
    [Owned]
    public class GroupSettings
    {
        public bool IsPrivate { get; set; } = true;
        public int MaxUsersAmount { get; set; } = 0;
        public bool MessagesOnlyForAdmins { get; set; } = false;

    }
}