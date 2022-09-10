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
    public virtual User User { get; init; }
    public virtual UserRoleAssociation Role { get; init; }
    public virtual ICollection<UserSkill> UserSkills { get; init; }
    public virtual ICollection<ProjectInfo> ProjectInfos { get; init; }
    public virtual ICollection<ReviewRequest> ReviewRequests { get; init; }
    public virtual ICollection<ReviewResponse> ReviewResponses { get; init; }
    public virtual ICollection<UserAchievementInfo> UserAchievements { get; init; }
}