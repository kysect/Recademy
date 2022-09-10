﻿using Recademy.Core.Types;
using Recademy.Dto.Projects;
using Recademy.Dto.Users;

using System.Collections.Generic;

namespace Recademy.Application.Services.Abstractions;

public interface IUserService
{
    RecademyUserDto GetById(int userId);
    RecademyUserDto FindById(int userId);
    RecademyUserDto FindRecademyUserByUsername(string username);
    UserInfoDto FindUserByUsername(string username);
    IReadOnlyCollection<ProjectInfoDto> GetProjectsByUserId(int userId);

    UserInfoDto UpdateUserRole(int adminId, int userId, UserType userType);
}