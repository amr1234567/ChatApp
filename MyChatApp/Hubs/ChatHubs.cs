using Microsoft.AspNetCore.SignalR;
using MyChatApp.Context;
using MyChatApp.Models;
using MyChatApp.Models.Dto;
using MyChatApp.Servieses;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyChatApp.Hubs
{
    public class ChatHubs : Hub
    {
        private readonly IMessgeServices messgeServices;
        private readonly IAccountServices accountServices;

        public ChatHubs(IMessgeServices messgeServices,IAccountServices accountServices)
        {
            this.messgeServices = messgeServices;
            this.accountServices = accountServices;
        }

        public async Task Sign(Guid userId)
        {
            await accountServices.AddConnectionIdToUser(userId, Context.ConnectionId);
            var groupsNames = await accountServices.GetGroupsOfUser(userId);
            if (groupsNames != null) 
                foreach (var item in groupsNames)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, item);
                }
        }

        public async Task SendMessage(Message message)
        {
            await messgeServices.SendMessage(message);
            await Clients.Group(message.Group.Name)
                .SendAsync("SendMessage", message.Sender.Name, message.Content);
        }

        public async Task DeleteMessage(string messageId)
        {

        }

        public async Task EditMessage(MessageForEditDto message)
        {

        }

        public async Task JoinGroup(User user,Guid GroupId)
        {
            await messgeServices.JoinGroup(user.Id, user.Name, GroupId);
            await accountServices.AddToGroup(user.Id, GroupId);
            await Clients.Group(user.Groups.FirstOrDefault(g => g.Id == GroupId).Name)
               .SendAsync("JoinGroup", user.Name, $"{user.Name} has joined the group");
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupId.ToString());
        }

        public async Task LeftGroup(User user, Guid GroupId)
        {
            await Clients.Group(user.Groups.FirstOrDefault(g => g.Id == GroupId).Name)
                .SendAsync("LeftGroup", user.Name, $"{user.Name} has left the group");
            await messgeServices.LeftGroup(user.Id, user.Name, GroupId);
            await accountServices.LeftGroup(user.Id, GroupId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupId.ToString());
        }

    }
}
