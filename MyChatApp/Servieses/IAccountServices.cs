using MyChatApp.Models;
using MyChatApp.Models.Dto;

namespace MyChatApp.Servieses
{
    public interface IAccountServices
    {
        Task AddConnectionIdToUser(Guid UserId, string ConnectionId);
        Task AddToGroup(Guid UserId, Guid GroupId);
        Task<List<string>?> GetGroupsOfUser(Guid UserId);
        Task<User?> GetUserById(Guid userId);
        Task LeftGroup(Guid UserId, Guid GroupId);
        Task<SignDetails> LogIn(UserLogInDto logInUser);
        Task<SignDetails> SignUp(UserLogInDto logInUser);
    }
}