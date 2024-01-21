using MyChatApp.Models;
using MyChatApp.Models.Dto;

namespace MyChatApp.Servieses
{
    public interface IAccountServices
    {
        Task<User?> GetUserById(Guid userId);
        Task<SignDetails> LogIn(UserLogInDto logInUser);
        Task<SignDetails> SignUp(UserLogInDto logInUser);
    }
}