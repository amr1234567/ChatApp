using Microsoft.EntityFrameworkCore;
using MyChatApp.Context;
using MyChatApp.Models;
using MyChatApp.Models.Dto;
using System.Text.RegularExpressions;

namespace MyChatApp.Servieses
{
    public class AccountServices : IAccountServices
    {
        private readonly ChatContext context;

        public AccountServices(ChatContext context)
        {
            this.context = context;
        }

        public async Task<SignDetails> LogIn(UserLogInDto logInUser)
        {
            var ReturnBox = new SignDetails();
            var user = await context.Users.FirstOrDefaultAsync(u => u.Name == logInUser.UserName && u.Password == logInUser.Password);
            if (user != null)
            {
                ReturnBox.Succesed = true;
                ReturnBox.UserId = user.Id;
            }
            else ReturnBox.messageDetails.Add("Password or UserName is Wrong");
            return ReturnBox;
        }
        public async Task<SignDetails> SignUp(UserLogInDto logInUser)
        {
            var ReturnBox = new SignDetails();
            var userCheck = await context.Users.FirstOrDefaultAsync(u => u.Name == logInUser.UserName);
            if (userCheck != null) ReturnBox.messageDetails.Add("UserName is Already Chosen");
            else
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = logInUser.UserName,
                    Password = logInUser.Password
                };
                ReturnBox.Succesed = true;
                ReturnBox.UserId = user.Id;
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            return ReturnBox;
        }
        public async Task<User?> GetUserById(Guid userId)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task AddToGroup(Guid UserId, Guid GroupId)
        {
            var user = await GetUserById(UserId);
            if (user != null)
            {
                var Group = await context.Groups.FirstOrDefaultAsync(g => g.Id == GroupId);
                if (Group != null)
                {
                    if (Group.Users == null)
                    {
                        Group.Users = new List<User>();
                        Group.Users.Add(user);
                    }
                    else
                        Group.Users.Add(user);
                    
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task LeftGroup(Guid UserId, Guid GroupId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if (user != null)
            {
                var Group = await context.Groups.FirstOrDefaultAsync(g => g.Id == GroupId);
                if (Group != null && Group.Users != null)
                {
                    if (Group.Users.Any(u => u.Id == UserId))
                    {
                        Group.Users.Remove(user);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
        public async Task AddConnectionIdToUser(Guid UserId, string ConnectionId)
        {
            var user =await GetUserById(UserId);
            if (user != null)
            {
                user.ConnectionId = ConnectionId;
                await context.SaveChangesAsync();
            }
        }
        public async Task<List<string>?> GetGroupsOfUser(Guid UserId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if (user != null && user.Groups != null) 
            {
                var groups = user.Groups.Select(g=> g.Name).ToList();
                return groups;
            }
            return null;
        }
    }
}
