﻿using System;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Achievements;
using Recademy.DataAccess;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recademy.Dto.Achievements;
using Recademy.Application.Mappings;

namespace Recademy.Application.Services.Implementations;

public sealed class UserAchievementService : IUserAchievementService
{
    private readonly IReadOnlyCollection<IUserAchievement> _achievements = new List<IUserAchievement>
    {
        new FirstTimeUserAchievement(),
        new NeatUserAchievement(),
    };

    private readonly RecademyContext _context;

    public UserAchievementService(RecademyContext context)
    {
        _context = context;
    }

    public IReadOnlyCollection<IUserAchievement> GetAllAchievements()
    {
        return _achievements;
    }

    public IReadOnlyCollection<IUserAchievement> GetUserAchievements(int userId)
    {
        HashSet<int> userAchievements = _context.UserAchievementInfos
            .Where(achievement => achievement.UserId == userId)
            .Select(achievement => achievement.AchievementId)
            .ToHashSet();

        return _achievements
            .Where(achievement => userAchievements.Contains(achievement.Id))
            .ToList();
    }

    public IReadOnlyCollection<UserAchievementRequestDto> GetUserAchievementRequests(int userId)
    {
        return _context.UserAchievementRequests
            .Where(request => request.UserId == userId)
            .Select(request => request.ToDto())
            .ToList();
    }

    public int GetUserAchievementPoints(int userId)
    {
        return GetUserAchievements(userId).Sum(achievement => achievement.Points);
    }

    public async Task AddUserAchievement(int userId, int achievementId)
    {
        _context.UserAchievementInfos.Add(new UserAchievementInfo { AchievementId = achievementId, UserId = userId });
        await _context.SaveChangesAsync();
    }

    public async Task AddUserAchievementRequest(UserAchievementRequestDto request)
    {
        UserAchievementRequest achievementRequest = request.FromDto();

        await _context.UserAchievementRequests.AddAsync(achievementRequest);
        await _context.SaveChangesAsync();
    }
}