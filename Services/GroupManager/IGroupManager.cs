using WebApplication5.Models;

namespace WebApplication5.Services.GroupManager
{
    public interface IGroupManager
    {
        Task AddUserToGroup(int groupId, int  userId);
        Task DeleteUserFromGroup(int groupId, int userId);
        Task UpdateGroupSettings(GroupSettings newSettings, int groupId);
    }
}
