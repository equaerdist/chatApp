using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Services.Repository.UserGroupsRepository
{
    public class SqlUserGroupsRepository : IUserGroupsRepository
    {
        private readonly ApplicationContext _context;

        public SqlUserGroupsRepository(ApplicationContext context) { _context = context; }
        public async Task AddAsync(UsersGroup userGroup)
        {
            await _context.UsersGroups.AddAsync(userGroup);
        }

        public Task DeleteAsync(UsersGroup oldGroup)
        {
            _context.UsersGroups.Remove(oldGroup);
            return Task.CompletedTask;
        }

        public async Task<UsersGroup?> GetAsync(int userId, int groupId)
        {
            var userGroup = await _context.UsersGroups
                .Include(ug => ug.Group)
                .FirstOrDefaultAsync(userGroup => userGroup.UserId == userId && userGroup.GroupId == groupId);
            return userGroup;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
