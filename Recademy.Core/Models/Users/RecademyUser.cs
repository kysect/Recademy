using Recademy.Core.Models.Achievements;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Models.Skills;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Recademy.Core.Models.Roles;

namespace Recademy.Core.Models.Users;

public class RecademyUser
{
    [Key]
    [ForeignKey("User")]
    public int UserId { get; init; }
    public User User { get; init; }
    public UserRoleAssociation Role { get; init; }
    public ICollection<UserSkill> UserSkills { get; init; }
    public ICollection<ProjectInfo> ProjectInfos { get; init; }
    public ICollection<ReviewRequest> ReviewRequests { get; init; }
    public ICollection<ReviewResponse> ReviewResponses { get; init; }
    public ICollection<UserAchievementInfo> UserAchievements { get; init; }
}