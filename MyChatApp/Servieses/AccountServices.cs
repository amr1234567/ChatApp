using Microsoft.EntityFrameworkCore;
using MyChatApp.Context;
using MyChatApp.Models;
using MyChatApp.Models.Dto;

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
                var user = new User() { Name = logInUser.UserName, Password = logInUser.Password };
                ReturnBox.UserId = user.Id;
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            return ReturnBox;
        }
        public async Task<User?> GetUserById(Guid userId)
        {
            return await context.Users.FindAsync(userId); ;
        }
    }
}
