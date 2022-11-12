using System;
using Recademy.Dto.Users;

namespace Recademy.Dto.Achievements;

public class UserAchievementRequestDto
{
    public int RequestId { get; init; }
    public int UserId { get; init; }
    public UserInfoDto User { get; init; }
    public int AchievementId { get; init; }
    public UserAchievementDto Achievement { get; init; }
    public string Reason { get; init; }
    public DateTime RequestTime { get; init; }
}