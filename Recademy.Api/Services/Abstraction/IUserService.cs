using System.Collections.Generic;
using Recademy.Library.Dto;

namespace Recademy.Api.Services.Abstraction
{
    public interface IUserService
    {
        UserInfoDto ReadUserInfo(int userId);
        UserInfoDto FindById(int userId);
        UserInfoDto FindByUsername(string username);
        List<ProjectInfoDto> ReadUserProjects(int userId);
        Dictionary<string, int> GetUsersRanking();
    }
}