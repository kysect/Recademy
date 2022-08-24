using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models;

public class UserAchievementInfo
{
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    public int AchievementId { get; set; }
}