using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Services.Repository 
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public SqlUserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User newUser)
        {
            await _context.Users.AddAsync(newUser);
        }

        public Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Group>?> GetGroupsForUserByIdAsync(int id, string? sortTerm, int page, int pageSize, string? sortOrder)
        {
            var keySelector = Pagination.Pagination<Group>.CreateKeySelector(sortTerm);

            var groupsQuery = _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.UserGroups)
                    .ThenInclude(us => us.Group)
                .Select(u => u.UserGroups)
                .SelectMany(us => us)
                .Select(us => us.Group);
            if (string.IsNullOrEmpty(sortOrder) || sortOrder.Equals("asc"))
                groupsQuery = groupsQuery.OrderBy(keySelector);
            else
                groupsQuery = groupsQuery.OrderByDescending(keySelector);

            return await groupsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        /// <summary>
        /// Ищет пользователя с заданным никнеймом в базе данных (регистронезависимый поиск).
        /// </summary>
        /// <param name="nickname">Никнейм пользователя для поиска.</param>
        /// <returns>Найденный пользователь или null, если пользователь не найден.</returns>
        public async Task<User?> GetUserByNickNameAsync(string nickname)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Nickname == nickname);
            return user;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }  
}