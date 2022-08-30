using System;
using System.ComponentModel.DataAnnotations;
using Recademy.Core.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Achievements;

public class UserAchievementRequest
{
    [Key]
    public int RequestId { get; set; }
    [ForeignKey("RecademyUser")]
    public int UserId { get; set; }
    public RecademyUser User { get; set; }
    public int AchievementId { get; set; }
    public string Reason { get; set; }
    public DateTime RequestTime { get; set; }
}