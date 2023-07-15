using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Services.Repository
{
    
    public class SqlMessageRepository : IMessageRepository
    {
        private readonly ApplicationContext _context;

        public SqlMessageRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddMessageToTheGroupAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task<IEnumerable<Message>?> GetMessagesForGroupByIdAsync(int id)
        {
            var group = await _context.Groups.Include(g => g.Messages).FirstOrDefaultAsync(g => g.Id == id);
            return group?.Messages.ToList();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}