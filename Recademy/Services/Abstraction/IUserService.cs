using System.Collections.Generic;
using Recademy.Dto;
using Recademy.Models;

namespace Recademy.Services.Abstraction
{
    public interface IUserService
    {
        User GetUserInfo(int userId);
        Dictionary<int, int> GetActivity(int userId);
        ProjectInfo AddProject(AddProjectDto argues);
    }
}