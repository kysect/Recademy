using System.Collections.Generic;
using Recademy.Application.Mappings;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Users;
using Recademy.Core.Tools;
using Recademy.Core.Types;
using Recademy.DataAccess.Repositories.Abstractions;
using Recademy.Shared.Dtos.Projects;
using Recademy.Shared.Dtos.Users;

namespace Recademy.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        public UserService(
            IUserRepository userRepository,
            IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        public RecademyUserDto ReadUserInfo(int userId)
        {
            return _userRepository
                .Get(userId)
                .ToDto();
        }

        public RecademyUserDto FindById(int userId)
        {
            return _userRepository
                .Find(userId)
                .ToDto();
        }

        public RecademyUserDto FindByUsername(string username)
        {
            return _userRepository
                .FindByUsername(username)
                .ToDto();
        }

        public List<ProjectInfoDto> ReadUserProjects(int userId)
        {
            RecademyUser user = _userRepository.Get(userId);

            return _projectRepository
                .FindByUser(user)
                .To(project => project.ToDto());
        }

        public UserInfoDto UpdateUserMentorRole(int adminId, int userId, UserType userType)
        {
            // TODO: Add method to get just User
            RecademyUser admin = _userRepository.Get(adminId);
            RecademyUser user = _userRepository.Get(userId);

            if (admin.User.UserType != UserType.Admin)
                throw RecademyException.NotEnoughPermission(adminId, admin.User.UserType, UserType.Admin);

            if (user.User.UserType == UserType.Admin)
                throw new RecademyException($"Cannot change role, user with id {userId} has admin role");

            if (userType == UserType.Admin)
                throw new RecademyException("Cannot set admin role. Action is not supported");

            return _userRepository
                .UpdateUserRole(user.User, userType)
                .ToDto();
        }
    }
}