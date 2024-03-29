﻿using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;
using Recademy.Dto.Achievements;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recademy.Api.Attributes;
using Recademy.Dto.Enums;
using System;

namespace Recademy.Api.Controllers;

[Route("api/achievements")]
[ApiController]
public class UserAchievementController : Controller
{
    private readonly IUserAchievementService _userAchievementService;

    public UserAchievementController(IUserAchievementService userAchievementService)
    {
        _userAchievementService = userAchievementService;
    }

    [HttpGet("users")]
    public ActionResult<IReadOnlyCollection<UserAchievementDto>> GetAllAchievements()
    {
        IReadOnlyCollection<UserAchievementDto> achievements = _userAchievementService
            .GetAllAchievements()
            .Select(achievement => new UserAchievementDto
            {
                Id = achievement.Id,
                Title = achievement.Title,
                Description = achievement.Description,
                Points = achievement.Points
            })
            .ToList();

        return Ok(achievements);
    }

    [HttpGet("users/{userId}")]
    public ActionResult<IReadOnlyCollection<UserAchievementDto>> GetUserAchievements(int userId)
    {
        IReadOnlyCollection<UserAchievementDto> achievements = _userAchievementService.GetUserAchievements(userId)
            .Select(achievement => new UserAchievementDto
            {
                Id = achievement.Id,
                Title = achievement.Title,
                Description = achievement.Description,
                Points = achievement.Points
            })
            .ToList();

        return Ok(achievements);
    }

    [HttpGet("users/ranged")]
    public ActionResult<IReadOnlyCollection<UserAchievementPointsDto>> GetRangedUserAchievements()
    {
        IReadOnlyCollection<UserAchievementPointsDto> achievementPoints = _userAchievementService.GetRangesUserAchievements();

        return Ok(achievementPoints);
    }

    [HttpGet("users/requests")]
    public async Task<ActionResult<IReadOnlyCollection<UserAchievementRequestDto>>> GetUserAchievementRequests()
    {
        IReadOnlyCollection<UserAchievementRequestDto> requests = await _userAchievementService.GetUserAchievementRequests();

        return Ok(requests);
    }

    [HttpGet("users/{userId}/requests")]
    public ActionResult<IReadOnlyCollection<UserAchievementRequestDto>> GetUserAchievementRequests(int userId)
    {
        IReadOnlyCollection<UserAchievementRequestDto> requests = _userAchievementService.GetUserAchievementRequests(userId);

        return Ok(requests);
    }

    [HttpGet("users/requests/{requestId}")]
    public async Task<ActionResult<UserAchievementRequestDto>> GetUserAchievementRequestById(int requestId)
    {
        UserAchievementRequestDto request = await _userAchievementService.GetUserAchievementRequestById(requestId);

        return Ok(request);
    }

    [HttpGet("users/{userId}/points")]
    public ActionResult<int> GetUserAchievementPoints(int userId)
    {
        int userAchievementPoints = _userAchievementService.GetUserAchievementPoints(userId);

        return Ok(userAchievementPoints);
    }

    [RoleRequired(UserTypeDto.Admin, UserTypeDto.Mentor)]
    [HttpPost("users/{userId}/{achievementId}")]
    public async Task<IActionResult> AddUserAchievement(int userId, int achievementId)
    {
        await _userAchievementService.AddUserAchievement(userId, achievementId);
        return Ok();
    }

    [HttpPost("users/{userId}/requests")]
    public async Task<IActionResult> RequestUserAchievement(UserAchievementRequestDto request)
    {
        try
        {
            await _userAchievementService.AddUserAchievementRequest(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [RoleRequired]
    [HttpPost("users/requests/{requestId}/responses")]
    public async Task<IActionResult> ResponseUserAchievement(UserAchievementResponseDto response)
    {
        await _userAchievementService.AddUserAchievementResponse(response);

        return Ok();
    }
}