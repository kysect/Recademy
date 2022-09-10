using Recademy.Core.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Achievements;

public class UserAchievementInfo
{
    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int AchievementId { get; set; }
}