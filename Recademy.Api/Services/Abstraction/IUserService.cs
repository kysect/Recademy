using System.Collections.Generic;
using Recademy.Shared.Dtos;
using Recademy.Core.Types;

namespace Recademy.Api.Services.Abstraction
{
    public interface IUserService
    {
        UserInfoDto ReadUserInfo(int userId);
        UserInfoDto FindById(int userId);
        UserInfoDto FindByUsername(string username);
        List<ProjectInfoDto> ReadUserProjects(int userId);

        UserInfoDto UpdateUserMentorRole(int adminId, int userId, UserType userType);
    }
}