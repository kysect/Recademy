using System.Collections.Generic;
using Recademy.Library.Dto;
using Recademy.Library.Models;

namespace Recademy.Api.Services.Abstraction
{
    public interface IUserService
    {
        UserInfoDto GetUserInfoDto(int userId);
        List<int> GetActivity(int userId);

        ProjectInfo AddProject(AddProjectDto argues);
        Dictionary<string, int> GetRanking();
    }
}