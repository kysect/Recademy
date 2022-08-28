using System.Collections.Generic;
using Recademy.Core.Models.Achievements;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Models.Skills;
using Recademy.Core.Types;

namespace Recademy.Shared.Dtos.Users;

public class RecademyUserDto
{
    public RecademyUserDto()
    {
    }

    public int UserId { get; init; }
    public string Name { get; init; }
    public string GithubUsername { get; init; }
    public UserType UserType { get; init; }
    public ICollection<UserSkill> UserSkills { get; init; }
    public ICollection<ProjectInfo> ProjectInfos { get; init; }
    public ICollection<ReviewRequest> ReviewRequests { get; init; }
    public ICollection<ReviewResponse> ReviewResponses { get; init; }
    public ICollection<UserAchievementInfo> UserAchievements { get; init; }
}