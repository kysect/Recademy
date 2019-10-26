using System.Collections.Generic;

namespace Recademy.Services.Abstraction
{
    public interface IAchievementService
    {
        List<Achievement> GetAchievements(int userId);
    }
}