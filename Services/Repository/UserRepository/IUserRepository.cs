using WebApplication5.Models;

namespace WebApplication5.Services.Repository 
{
    public interface IUserRepository 
    {
        Task AddUserAsync(User newUser);
        Task<User?> GetUserByNickNameAsync(string nickname);
        Task<User?> GetUserByIdAsync(int id);
        Task DeleteUserAsync(User user);
        Task<IEnumerable<Group>?> GetGroupsForUserByIdAsync(int id, string? sortTerm, int page, int pageSize, string? sortOrded);
        Task SaveChangesAsync();
    }
}