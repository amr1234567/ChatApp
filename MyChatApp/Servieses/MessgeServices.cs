using Microsoft.EntityFrameworkCore;
using MyChatApp.Context;
using MyChatApp.Models;
using MyChatApp.Models.Dto;

namespace MyChatApp.Servieses
{
    public class MessgeServices : IMessgeServices
    {
        private readonly ChatContext context;

        public MessgeServices(ChatContext context)
        {
            this.context = context;
        }

        public async Task SendMessage(Message message)
        {
            if (message != null)
            {
                await context.Messages.AddAsync(message);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteMessage(Message message)
        {
            if (message != null)
            {
                var mess = await context.Messages.FirstOrDefaultAsync(m => m.Id == message.Id);
                if (mess != null)
                {
                    context.Messages.Remove(mess);
                    await context.SaveChangesAsync();
                }

            }
        }
        public async Task EditMessage(MessageForEditDto message)
        {
            if (message != null)
            {
                var mess = await context.Messages.FirstOrDefaultAsync(m => m.Id == message.MessageId);
                if (mess != null)
                {
                    mess.Content = message.NewMessage;
                    await context.SaveChangesAsync();
                }

            }
        }
        public async Task<bool> CheckUserInGroup(Guid UserId, Guid GroupId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if (user != null && user.Groups != null)
            {
                var Group = user.Groups.FirstOrDefault(g => g.Id == GroupId);
                if (Group != null) return true;
            }
            return false;
        }

        public async Task JoinGroup(Guid UserId,string userName, Guid GroupId)
        {
            context.Messages
                    .Add(new Message()
                    {
                        Id = Guid.NewGuid(),
                        GroupId = GroupId,
                        SenderId = UserId,
                        Content = $"{userName} has joined the group"
                    });
            await context.SaveChangesAsync();
        }
        public async Task LeftGroup(Guid UserId, string userName, Guid GroupId)
        {
            context.Messages
                    .Add(new Message()
                    {
                        Id = Guid.NewGuid(),
                        GroupId = GroupId,
                        SenderId = UserId,
                        Content = $"{userName} has joined the group"
                    });
            await context.SaveChangesAsync();
        }
    }
}