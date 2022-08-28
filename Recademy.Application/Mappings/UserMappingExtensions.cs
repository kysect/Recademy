using Recademy.Core.Models.Users;
using Recademy.Shared.Dtos.Users;

namespace Recademy.Application.Mappings;

public static class UserMappingExtensions
{
    public static UserInfoDto ToDto(this User user)
    {
        return new UserInfoDto()
        {
            Id = user.Id,
            Name = user.Name,
            GithubUsername = user.GithubUsername,
            UserType = user.UserType,
        };
    }

    public static RecademyUserDto ToDto(this RecademyUser recademyUser)
    {
        return new RecademyUserDto()
        {
            UserId = recademyUser.User.Id,
            Name = recademyUser.User.Name,
            GithubUsername = recademyUser.User.GithubUsername,
            UserType = recademyUser.User.UserType,
            UserSkills = recademyUser.UserSkills,
            ProjectInfos = recademyUser.ProjectInfos,
            ReviewRequests = recademyUser.ReviewRequests,
            ReviewResponses = recademyUser.ReviewResponses,
            UserAchievements = recademyUser.UserAchievements,
        };
    }
}