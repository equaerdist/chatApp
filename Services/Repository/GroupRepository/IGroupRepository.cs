using System.Collections;
using WebApplication5.Models;

namespace WebApplication5.Services.Repository
{
    public interface IGroupRepository
    {
        Task<IEnumerable<User>?> GetUsersInGroupByIdAsync(int id, int pageSize, int page);
        Task<Group?> GetGroupByIdAsync(int id);
        Task AddGroupAsync(Group newGroup);
        Task DeleteGroupAsync(Group oldGroup);
        Task SaveChangesAsync();
        Task<bool> GroupContainsUser(int userId, int groupId);
        Task<IEnumerable<Message>> GetMessagesForGroupByIdAsync(int id, int pageSize, int page);
        Task<IEnumerable<Group>> GetGroupBySearchTermAsync(string searchTerm);
        Task<IEnumerable<UsersGroup>?> GetOnlineUsersForGroup(int groupId);
    }
}