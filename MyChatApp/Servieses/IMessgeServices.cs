﻿using MyChatApp.Models;
using MyChatApp.Models.Dto;

namespace MyChatApp.Servieses
{
    public interface IMessgeServices
    {
        Task<bool> CheckUserInGroup(Guid UserId, Guid GroupId);
        Task DeleteMessage(Message message);
        Task EditMessage(MessageForEditDto message);
        Task JoinGroup(Guid UserId, Guid GroupId);
        Task LeftGroup(Guid UserId, Guid GroupId);
        Task SendMessage(Message message);
    }
}