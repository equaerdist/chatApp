using WebApplication5.Models;

namespace WebApplication5.Services.Repository.UserGroupsRepository
{
    public interface IUserGroupsRepository
    {
        Task<UsersGroup?> GetAsync(int userId, int groupId);
        Task AddAsync(UsersGroup userGroup);
        Task DeleteAsync(UsersGroup oldGroup);
        Task SaveChangesAsync();
    }
}
