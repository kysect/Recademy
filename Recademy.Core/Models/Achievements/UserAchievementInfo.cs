using Recademy.Core.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Achievements;

public class UserAchievementInfo
{
    [ForeignKey("RecademyUser")]
    public int UserId { get; set; }
    public virtual RecademyUser User { get; set; }
    public int AchievementId { get; set; }
}