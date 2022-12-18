using System;

namespace Recademy.Dto.Enums;

public enum UserTypeDto
{
    CommonUser = 1,
    Mentor = 2,
    Admin = 3,
}

public static class UserTypeExtensions
{
    public static string TranslateToString(this UserTypeDto userType)
    {
        return userType switch
        {
            UserTypeDto.CommonUser => "Пользователь",
            UserTypeDto.Mentor => "Ментор",
            UserTypeDto.Admin => "Администратор",
            _ => throw new ArgumentOutOfRangeException(nameof(userType), userType, null)
        };
    }
}