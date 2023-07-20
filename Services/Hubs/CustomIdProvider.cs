using Microsoft.AspNetCore.SignalR;

namespace WebApplication5.Services.Hubs
{
    public class CustomIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(x => x.Type == "Id")?.Value;
        }
    }
}
