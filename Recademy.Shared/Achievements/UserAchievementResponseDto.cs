using System;
using Recademy.Dto.Enums;

namespace Recademy.Dto.Achievements;

public class UserAchievementResponseDto
{
    public UserAchievementResponseDto()
    {
    }

    public int ResponseId { get; init; }
    public int RequestId { get; init; }
    public UserAchievementResponseTypeDto Response { get; init; }
    public string Comment { get; init; }
    public DateTime ResponseTime { get; init; }
}