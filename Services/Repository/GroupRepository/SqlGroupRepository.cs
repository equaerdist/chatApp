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
            return await _context.Groups.Include(g => g.UsersGroup).Include(g => g.Messages).FirstOrDefaultAsync(g => g.Id == id);
        }



        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}