namespace Recademy.Shared.Dtos;

public sealed class UserAchievementDto
{
    public int Id { get; }
    public string Title { get; }
    public string Description { get; }
    public int Points { get; }

    public UserAchievementDto(int id, string title, string description, int points)
    {
        Id = id;
        Title = title;
        Description = description;
        Points = points;
    }
}