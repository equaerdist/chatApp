using WebApplication5.Models;

namespace WebApplication5.Services.Repository
{
    public interface IMessageRepository
    {
        Task AddMessageToTheGroupAsync(Message message);
        Task<IEnumerable<Message>> GetMessagesForGroupByIdAsync(int id);
    }
}