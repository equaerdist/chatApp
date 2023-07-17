using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Services.Repository
{
    public class SqlGroupRepository : IGroupRepository
    {
        private readonly ApplicationContext _context;

        public SqlGroupRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddGroupAsync(Group newGroup)
        {
            await _context.Groups.AddAsync(newGroup);
        }

        public Task DeleteGroupAsync(Group oldGroup)
        {
            _context.Groups.Remove(oldGroup);
            return Task.CompletedTask;
        }


        public async Task<Group?> GetGroupByIdAsync(int id)
        {
            return await _context.Groups
                .Include(g => g.Messages)
                .ThenInclude(m => m.Creator)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Message>> GetMessagesForGroupByIdAsync(int id, int pageSize, int page)
        {
            var messages = await _context.Messages
                .Include(m => m.Creator)
                .Where(m => m.GroupId == id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return messages;
        }

        public async Task<bool> GroupContainsUser(int userId, int groupId)
        {
           var user = await _context.UsersGroups.FirstOrDefaultAsync(ug => ug.GroupId == groupId && ug.UserId == userId);
            return user != null;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}