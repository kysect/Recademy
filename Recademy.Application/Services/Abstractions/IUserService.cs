using System.Collections.Generic;
using Recademy.Core.Types;
using Recademy.Shared.Dtos.Projects;
using Recademy.Shared.Dtos.Users;

namespace Recademy.Application.Services.Abstractions
{
    public interface IUserService
    {
        RecademyUserDto ReadUserInfo(int userId);
        RecademyUserDto FindById(int userId);
        RecademyUserDto FindByUsername(string username);
        List<ProjectInfoDto> ReadUserProjects(int userId);

        UserInfoDto UpdateUserMentorRole(int adminId, int userId, UserType userType);
    }
}