using System.Collections.Generic;
using Recademy.BlazorWeb.Dto;
using Recademy.BlazorWeb.Models;

namespace Recademy.BlazorWeb.Services.Abstraction
{
    public interface IUserService
    {
        UserInfoDto GetUserInfoDto(int userId);
        List<int> GetActivity(int userId);

        ProjectInfo AddProject(AddProjectDto argues);
        Dictionary<string, int> GetRanking();
    }
}