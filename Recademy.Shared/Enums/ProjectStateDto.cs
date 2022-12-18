using System;

namespace Recademy.Dto.Enums;

public enum ProjectStateDto
{
    Requested = 1,
    Reviewed = 2,
    Completed = 3,
    Abandoned = 4,
}

public static class ProjectStateExtensions
{
    public static string TranslateToString(this ProjectStateDto state)
    {
        return state switch
        {
            ProjectStateDto.Requested => "Запрошено ревью",
            ProjectStateDto.Reviewed => "Получено ревью",
            ProjectStateDto.Completed => "Завершено",
            ProjectStateDto.Abandoned => "Отклонено",
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }
}