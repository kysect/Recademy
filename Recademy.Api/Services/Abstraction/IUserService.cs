using System.Collections.Generic;
using Recademy.Library.Dto;

namespace Recademy.Api.Services.Abstraction
{
    public interface IUserService
    {
        UserInfoDto GetUserInfo(int userId);
        Dictionary<string, int> GetUsersRanking();
    }
}