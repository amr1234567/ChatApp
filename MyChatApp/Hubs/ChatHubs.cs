using Microsoft.AspNetCore.SignalR;
using MyChatApp.Models;
using MyChatApp.Models.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyChatApp.Hubs
{
    public class ChatHubs : Hub
    {
        public async Task JoinChat(UserConnection connection)
        {
            await Clients.All.SendAsync("RecieveMessage", "admin", $"{connection.UserName} has joined");
        }

        public async Task SendMessage(Message message)
        {

        }
        public async Task DeleteMessage(string messageId)
        {

        }
        public async Task EditMessage(MessageForEditDto message)
        {

        }
        public async Task JoinGroup(User user,string GroupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupId);
        }
        public async Task LeftGroup(User user)
        {

        }

        public async Task JoinSpecificGroup(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatGroupName);
            await Clients.Group(connection.ChatGroupName)
                .SendAsync("JoinSpecificGroup", "admin", $"{connection.UserName} has joined the group");
        }

    }
}
