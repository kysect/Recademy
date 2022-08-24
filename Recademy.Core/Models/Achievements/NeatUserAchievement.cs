namespace Recademy.Core.Models.Achievements;

public sealed class NeatUserAchievement : IUserAchievement
{
    public int Id => 2;
    public string Title => "Чистоплотный";
    public string Description => "Соблюдает правила кодстайла";
    public int Points => 10;
}