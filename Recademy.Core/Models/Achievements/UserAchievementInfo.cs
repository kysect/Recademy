using System.ComponentModel.DataAnnotations.Schema;
using Recademy.Core.Models.Users;

namespace Recademy.Core.Models.Achievements;

public class UserAchievementInfo
{
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    public int AchievementId { get; set; }
}