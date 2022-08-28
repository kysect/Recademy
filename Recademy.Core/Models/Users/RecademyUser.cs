using Recademy.Core.Models.Achievements;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Models.Skills;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Users;

public class RecademyUser
{
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<UserSkill> UserSkills { get; set; }
    public ICollection<ProjectInfo> ProjectInfos { get; set; }
    public ICollection<ReviewRequest> ReviewRequests { get; set; }
    public ICollection<ReviewResponse> ReviewResponses { get; set; }
    public ICollection<UserAchievementInfo> UserAchievements { get; set; }
}