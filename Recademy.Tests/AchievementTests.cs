using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Recademy.Application.Services.Abstractions;
using Recademy.Application.Services.Implementations;
using Recademy.Core.Models.Users;
using Recademy.Core.Types;
using Recademy.DataAccess;
using Recademy.DataAccess.Seeding;
using Recademy.Dto.Achievements;
using Recademy.Dto.Enums;
using System;
using System.Threading.Tasks;

namespace Recademy.Tests;

public sealed class AchievementTests : IDisposable
{
    private RecademyContext _context;

    private IUserAchievementService _userAchievementService;

    [OneTimeSetUp]
    public void SetUp()
    {
        _context = new RecademyContext(new DbContextOptionsBuilder<RecademyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseLazyLoadingProxies()
                .Options,
            new DbContextSeeder());

        _context.Database.EnsureCreated();

        _userAchievementService = new UserAchievementService(_context);
    }

    [Test]
    public async Task CreateAchievementRequest_RequestExists()
    {
        UserAchievementRequestDto createdAchievementRequest = await CreateAchievementRequest();
        UserAchievementRequestDto achievementRequest = await _userAchievementService.GetUserAchievementRequestById(createdAchievementRequest.RequestId);

        Assert.IsNotNull(achievementRequest);
    }

    [Test]
    public async Task CreateAchievementResponse_ResponseExists()
    {
        UserAchievementResponseDto createdAchievementResponse = await CreateAchievementResponse();
        UserAchievementResponseDto achievementResponse = await _userAchievementService.GetUserAchievementResponse(createdAchievementResponse.RequestId);

        Assert.IsNotNull(achievementResponse);
    }

    private async Task<UserAchievementRequestDto> CreateAchievementRequest()
    {
        var userAchievementRequest = new UserAchievementRequestDto
        {
            UserId = 1,
            AchievementId = 1,
            Reason = string.Empty,
            RequestTime = DateTime.UtcNow,
        };

        UserAchievementRequestDto createdAchievementRequest = await _userAchievementService.AddUserAchievementRequest(userAchievementRequest);

        return createdAchievementRequest;
    }

    private async Task<UserAchievementResponseDto> CreateAchievementResponse()
    {
        var mentor = new User
        {
            GithubUsername = "Mentor",
            Name = "Mentor",
            UserType = UserType.Mentor
        };

        User user = _context.Users.Add(mentor).Entity;
        await _context.SaveChangesAsync();

        UserAchievementRequestDto achievementRequest = await CreateAchievementRequest();

        var achievementResponse = new UserAchievementResponseDto
        {
            RequestId = achievementRequest.RequestId,
            Response = UserAchievementResponseTypeDto.Approved,
            Comment = string.Empty,
            ResponseTime = DateTime.UtcNow,
        };

        UserAchievementResponseDto createdAchievementResponse = await _userAchievementService.AddUserAchievementResponse(achievementResponse);

        return createdAchievementResponse;
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}