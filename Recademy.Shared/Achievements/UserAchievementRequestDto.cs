using System;

namespace Recademy.Dto.Achievements;

public class UserAchievementRequestDto
{
    public UserAchievementRequestDto()
    {
    }

    public int RequestId { get; init; }
    public int UserId { get; init; }
    public int AchievementId { get; init; }
    public string Reason { get; init; }
    public DateTime RequestTime { get; init; }
}