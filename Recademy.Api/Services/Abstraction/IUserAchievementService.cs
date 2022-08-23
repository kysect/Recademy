using System.Collections.Generic;
using Recademy.Core.Models.Achievements;

namespace Recademy.Api.Services.Abstraction;

public interface IUserAchievementService
{
    IReadOnlyCollection<IUserAchievement> GetAllAchievements();
}