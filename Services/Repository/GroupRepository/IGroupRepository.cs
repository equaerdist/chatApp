using WebApplication5.Models;

namespace WebApplication5.Services.Repository
{
    public interface IGroupRepository
    {
        //Task<IEnumerable<User>?> GetUsersInGroupByIdAsync(int id);
        Task<Group?> GetGroupByIdAsync(int id);
        Task AddGroupAsync(Group newGroup);
        Task DeleteGroupAsync(Group oldGroup);
        Task SaveChangesAsync();
    }
}