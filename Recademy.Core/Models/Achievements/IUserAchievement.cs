namespace Recademy.Core.Models.Achievements;

public interface IUserAchievement
{
    public int Id { get; }
    public string Title { get; }
    public string Description { get; }
    public int Points { get; }
}