using System.Collections.Generic;
using Recademy.Api.Repositories;
using Recademy.Api.Services.Abstraction;
using Recademy.Api.Tools;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Library.Tools;
using Recademy.Library.Types;

namespace Recademy.Api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IAchievementService _achievements;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IGithubApiAccessor _githubApiAccessor;

        public UserService(
            IAchievementService achievements,
            IUserRepository userRepository,
            IProjectRepository projectRepository,
            IGithubApiAccessor githubApiAccessor)
        {
            _achievements = achievements;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _githubApiAccessor = githubApiAccessor;
        }

        public UserInfoDto ReadUserInfo(int userId)
        {
            return _userRepository
                .Get(userId)
                .To(ConvertToDto);
        }

        public UserInfoDto FindById(int userId)
        {
            return _userRepository
                .Find(userId)
                .Maybe(ConvertToDto);
        }

        public UserInfoDto FindByUsername(string username)
        {
            return _userRepository
                .FindByUsername(username)
                .Maybe(ConvertToDto);
        }

        public List<ProjectInfoDto> ReadUserProjects(int userId)
        {
            User user = _userRepository.Get(userId);

            return _projectRepository
                .FindByUser(user)
                .To(p => new ProjectInfoDto(p));
        }

        public UserInfoDto UpdateUserMentorRole(int adminId, int userId, UserType userType)
        {
            User admin = _userRepository.Get(adminId);
            User user = _userRepository.Get(userId);

            if (admin.UserType != UserType.Admin)
                throw RecademyException.NotEnoughPermission(adminId, admin.UserType, UserType.Admin);

            if (user.UserType == UserType.Admin)
                throw new RecademyException($"Cannot change role, user with id {userId} has admin role");

            if (userType == UserType.Admin)
                throw new RecademyException("Cannot set admin role. Action is not supported");

            return _userRepository
                .UpdateUserRole(user, userType)
                .To(ConvertToDto);
        }

        private UserInfoDto ConvertToDto(User user)
        {
            return new UserInfoDto(user)
            {
                Activities = _achievements.GetUserActivityPerMonth(user.Id),
                Achievements = _achievements.GetAchievements(user),
                GithubInfo = _githubApiAccessor.GetUserProfile(user.GithubLink)
            };
        }
    }
}