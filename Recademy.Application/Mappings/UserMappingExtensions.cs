using Recademy.Core.Models.Users;
using Recademy.Core.Types;
using Recademy.Dto.Enums;
using Recademy.Dto.Users;

using System;
using System.Linq;

namespace Recademy.Application.Mappings;

public static class UserMappingExtensions
{
    public static UserInfoDto ToDto(this User user)
    {
        if (user is null)
            return null;

        return new UserInfoDto
        {
            Id = user.Id,
            Name = user.Name,
            GithubUsername = user.GithubUsername,
            UserType = user.UserType.ToDto(),
        };
    }

    public static User FromDto(this UserInfoDto user)
    {
        if (user is null)
            return null;

        return new User
        {
            Id = user.Id,
            Name = user.Name,
            GithubUsername = user.GithubUsername,
            UserType = user.UserType.FromDto(),
        };
    }

    public static RecademyUserDto ToDto(this RecademyUser recademyUser)
    {
        if (recademyUser is null)
            return null;

        return new RecademyUserDto
        {
            UserId = recademyUser.User.Id,
            User = recademyUser.User.ToDto(),
            UserSkills = recademyUser.UserSkills.Select(skill => skill.ToDto()).ToList(),
            ProjectInfos = recademyUser.ProjectInfos.Select(project => project.ToDto()).ToList(),
            ReviewRequests = recademyUser.ReviewRequests.Select(request => request.ToDto()).ToList(),
            ReviewResponses = recademyUser.ReviewResponses.Select(response => response.ToDto()).ToList(),
            UserAchievements = recademyUser.UserAchievements.Select(achievement => achievement.ToDto()).ToList(),
        };
    }

    public static RecademyUser FromDto(this RecademyUserDto recademyUser)
    {
        if (recademyUser is null)
            return null;

        return new RecademyUser
        {
            UserId = recademyUser.UserId,
            User = recademyUser.User.FromDto(),
            UserSkills = recademyUser.UserSkills.Select(skill => skill.FromDto()).ToList(),
            ProjectInfos = recademyUser.ProjectInfos.Select(project => project.FromDto()).ToList(),
            ReviewRequests = recademyUser.ReviewRequests.Select(request => request.FromDto()).ToList(),
            ReviewResponses = recademyUser.ReviewResponses.Select(response => response.FromDto()).ToList(),
            UserAchievements = recademyUser.UserAchievements.Select(achievement => achievement.FromDto(recademyUser.UserId)).ToList(),
        };
    }

    public static UserTypeDto ToDto(this UserType userType)
    {
        return userType switch
        {
            UserType.CommonUser => UserTypeDto.CommonUser,
            UserType.Mentor => UserTypeDto.Mentor,
            UserType.Admin => UserTypeDto.Admin,
            _ => throw new ArgumentOutOfRangeException(nameof(userType), userType, null)
        };
    }

    public static UserType FromDto(this UserTypeDto userType)
    {
        return userType switch
        {
            UserTypeDto.CommonUser => UserType.CommonUser,
            UserTypeDto.Mentor => UserType.Mentor,
            UserTypeDto.Admin => UserType.Admin,
            _ => throw new ArgumentOutOfRangeException(nameof(userType), userType, null)
        };
    }
}