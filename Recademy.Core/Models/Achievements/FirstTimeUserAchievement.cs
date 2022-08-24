namespace Recademy.Core.Models.Achievements;

public sealed class FirstTimeUserAchievement : IUserAchievement
{
    public int Id => 1;
    public string Title => "Первый раз";
    public string Description => "1-ый пулл-реквест в секту";
    public int Points => 5;
}