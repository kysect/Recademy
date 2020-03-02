using System.Collections.Generic;
using Recademy.Library.Dto;

namespace Recademy.Api.Services.Abstraction
{
    //TODO: add method FindById
    //TODO: add method FindByUsername
    public interface IUserService
    {
        UserInfoDto GetUserInfo(int userId);
        Dictionary<string, int> GetUsersRanking();
    }
}