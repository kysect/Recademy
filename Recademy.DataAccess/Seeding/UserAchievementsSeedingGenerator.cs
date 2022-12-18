using Microsoft.EntityFrameworkCore;
using Recademy.Core.Models.Achievements;
using System.Collections.Generic;
using System;

namespace Recademy.DataAccess.Seeding;

public class UserAchievementsSeedingGenerator : IEntitySeedingGenerator
{
    private readonly List<UserAchievementInfo> _userAchievements;

    public UserAchievementsSeedingGenerator(int count)
    {
        _userAchievements = new List<UserAchievementInfo>();

        for (int userId = 1; userId <= count; userId++)
        {
            var userAchievementInfo = new UserAchievementInfo()
            {
                AchievementId = 1,
                UserId = userId,
            };

            _userAchievements.Add(userAchievementInfo);
        }
    }

    public void Seed(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.Entity<UserAchievementInfo>().HasData(_userAchievements);
    }
}