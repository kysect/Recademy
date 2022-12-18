using System;

namespace Recademy.Dto.Enums;

public enum UserAchievementResponseTypeDto
{
    Approved,
    Declined,
    NoResponse,
}

public static class UserAchievementResponseTypeExtensions
{
    public static string TranslateToString(this UserAchievementResponseTypeDto response)
    {
        return response switch
        {
            UserAchievementResponseTypeDto.Approved => "Одобрено",
            UserAchievementResponseTypeDto.Declined => "Отклонено",
            UserAchievementResponseTypeDto.NoResponse => "Без ответа",
            _ => throw new ArgumentOutOfRangeException(nameof(response), response, null)
        };
    }
}