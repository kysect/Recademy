using Recademy.Core.Types;
using Recademy.Dto.Projects;
using Recademy.Dto.Users;

using System.Collections.Generic;

namespace Recademy.Application.Services.Abstractions
{
    public interface IUserService
    {
        RecademyUserDto ReadUserInfo(int userId);
        RecademyUserDto FindById(int userId);
        RecademyUserDto FindRecademyUser(string username);
        UserInfoDto FindUser(string username);
        List<ProjectInfoDto> ReadUserProjects(int userId);

        UserInfoDto UpdateUserMentorRole(int adminId, int userId, UserType userType);
    }
}