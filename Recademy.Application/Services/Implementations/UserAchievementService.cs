using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Achievements;
using Recademy.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recademy.Dto.Achievements;
using Recademy.Application.Mappings;
using Recademy.Application.Providers;
using Recademy.Core.Types;

namespace Recademy.Application.Services.Implementations;

public sealed class UserAchievementService : IUserAchievementService
{
    private readonly RecademyContext _context;

    public UserAchievementService(RecademyContext context)
    {
        _context = context;
    }

    public IReadOnlyCollection<IUserAchievement> GetAllAchievements()
    {
        return UserAchievementProvider.Achievements;
    }

    public IReadOnlyCollection<IUserAchievement> GetUserAchievements(int userId)
    {
        var userAchievements = _context.UserAchievementInfos
            .Where(achievement => achievement.UserId == userId)
            .Select(achievement => achievement.AchievementId)
            .ToHashSet();

        return UserAchievementProvider.Achievements
            .Where(achievement => userAchievements.Contains(achievement.Id))
            .ToList();
    }

    public IReadOnlyCollection<UserAchievementPointsDto> GetRangesUserAchievements()
    {
        var rangedUserAchievementPoints = new List<UserAchievementPointsDto>();

        IReadOnlyCollection<UserAchievementInfo> userAchievementsInfo = _context.UserAchievementInfos.ToList();

        foreach (IGrouping<string, UserAchievementInfo> userAchievementInfo in userAchievementsInfo.GroupBy(info => info.User.User.GithubUsername))
        {
            int userPoints = userAchievementInfo
                .Select(info => UserAchievementProvider.FindAchievementById(info.AchievementId))
                .Sum(achievement => achievement.Points);

            var userAchievementPoints = new UserAchievementPointsDto(Username: userAchievementInfo.Key, userPoints);

            rangedUserAchievementPoints.Add(userAchievementPoints);
        }

        return rangedUserAchievementPoints.OrderByDescending(info => info.Points).ToList();
    }

    public async Task<IReadOnlyCollection<UserAchievementRequestDto>> GetUserAchievementRequests()
    {
        return await _context.UserAchievementRequests
            .Include(request => request.User)
            .ThenInclude(recademyUser => recademyUser.User)
            .Select(achievement => achievement.ToDto())
            .ToListAsync();
    }

    public IReadOnlyCollection<UserAchievementRequestDto> GetUserAchievementRequests(int userId)
    {
        return _context.UserAchievementRequests
            .Where(request => request.UserId == userId)
            .Select(request => request.ToDto())
            .ToList();
    }

    public async Task<UserAchievementRequestDto> GetUserAchievementRequestById(int requestId)
    {
        UserAchievementRequest request = await _context.UserAchievementRequests
            .Include(request => request.User)
            .ThenInclude(recademyUser => recademyUser.User)
            .FirstOrDefaultAsync(request => request.RequestId == requestId);

        return request.ToDto();
    }

    public int GetUserAchievementPoints(int userId)
    {
        return GetUserAchievements(userId).Sum(achievement => achievement.Points);
    }

    public async Task<UserAchievementResponseDto> GetUserAchievementResponse(int requestId)
    {
        UserAchievementResponse response = await _context.UserAchievementResponses
            .FirstOrDefaultAsync(response => response.RequestId == requestId);

        return response.ToDto();
    }

    public async Task AddUserAchievement(int userId, int achievementId)
    {
        _context.UserAchievementInfos.Add(new UserAchievementInfo { AchievementId = achievementId, UserId = userId });
        await _context.SaveChangesAsync();
    }

    public async Task<UserAchievementRequestDto> AddUserAchievementRequest(UserAchievementRequestDto request)
    {
        bool isRequestAlreadyExists = await _context.UserAchievementRequests
            .AnyAsync(r => r.AchievementId == request.AchievementId && r.UserId == request.UserId);

        if (isRequestAlreadyExists)
            throw new RecademyException($"Request of achievement {request.AchievementId} by user {request.UserId} already exists");

        bool isUserAlreadyHasAchievement = await _context.UserAchievementInfos
            .AnyAsync(a => a.AchievementId == request.AchievementId && a.UserId == request.UserId);

        if (isUserAlreadyHasAchievement)
            throw new RecademyException($"User {request.UserId} already has achievement {request.AchievementId}");

        UserAchievementRequest achievementRequest = request.FromDto();

        await _context.UserAchievementRequests.AddAsync(achievementRequest);
        await _context.SaveChangesAsync();

        achievementRequest = await _context.UserAchievementRequests
            .Include(r => r.User)
            .FirstAsync(r => r.RequestId == achievementRequest.RequestId);

        return achievementRequest.ToDto();
    }

    public async Task<UserAchievementResponseDto> AddUserAchievementResponse(UserAchievementResponseDto response)
    {
        UserAchievementResponse achievementResponse = response.FromDto();

        UserAchievementRequest achievementRequest = await _context.UserAchievementRequests
            .FirstAsync(request => request.RequestId == achievementResponse.RequestId);

        await _context.UserAchievementResponses.AddAsync(achievementResponse);
        _context.UserAchievementRequests.Remove(achievementRequest);

        await _context.SaveChangesAsync();

        if (achievementResponse.Response is UserAchievementResponseType.Approved)
            await AddUserAchievement(achievementRequest.UserId, achievementRequest.AchievementId);

        achievementResponse = await _context.UserAchievementResponses
                .FirstAsync(response => response.ResponseId == achievementResponse.ResponseId);

        return achievementResponse.ToDto();
    }
}