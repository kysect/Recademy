namespace Recademy.Dto.Achievements;

public sealed class UserAchievementDto
{
    public UserAchievementDto()
    {
    }

    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public int Points { get; init; }
}