using System;

namespace Recademy.Dto.Enums;

public enum ReviewConclusionDto
{
    LooksGood = 1,
    WithComments = 2,
    NeedWork = 3,
}

public static class ReviewConclusionExtensions
{
    public static string TranslateToString(this ReviewConclusionDto reviewConclusion)
    {
        return reviewConclusion switch
        {
            ReviewConclusionDto.LooksGood => "Всё супер",
            ReviewConclusionDto.WithComments => "Есть замечания",
            ReviewConclusionDto.NeedWork => "Нужно доработать",
            _ => throw new ArgumentOutOfRangeException(nameof(reviewConclusion), reviewConclusion, null)
        };
    }
}