using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Recademy.Core.Types;

namespace Recademy.Core.Models.Achievements;

public class UserAchievementResponse
{
    [Key]
    public int ResponseId { get; set; }
    public int RequestId { get; set; }
    [ForeignKey("UserAchievementRequest")]
    public UserAchievementRequest Request { get; set; }
    public UserAchievementResponseType Response { get; set; }
    public string Comment { get; set; }
    public DateTime ResponseTime { get; set; }
}