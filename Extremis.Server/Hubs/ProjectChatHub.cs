using Extremis.ProjectChats;
using Microsoft.AspNetCore.SignalR;

namespace Extremis.Hubs;

public class ProjectChatHub : Hub
{
    public Task SendMessage(MessageDto message, string userName)
    {
        return Clients.All.SendAsync("ReceiveMessage", message, userName);
    }
    
    public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
    {
        await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
    }
}