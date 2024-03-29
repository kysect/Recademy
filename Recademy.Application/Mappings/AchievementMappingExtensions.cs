﻿using System;
using Recademy.Application.Providers;
using Recademy.Core.Models.Achievements;
using Recademy.Core.Types;
using Recademy.Dto.Achievements;
using Recademy.Dto.Enums;

namespace Recademy.Application.Mappings;

public static class AchievementMappingExtensions
{
    public static UserAchievementDto ToDto(this UserAchievementInfo achievement)
    {
        if (achievement is null)
            return null;

        IUserAchievement userAchievement = UserAchievementProvider.FindAchievementById(achievement.AchievementId);

        return new UserAchievementDto
        {
            Id = achievement.AchievementId,
            Title = userAchievement.Title,
            Points = userAchievement.Points,
            Description = userAchievement.Description,
        };
    }

    public static UserAchievementDto ToDto(this IUserAchievement achievement)
    {
        if (achievement is null)
            return null;

        return new UserAchievementDto
        {
            Id = achievement.Id,
            Title = achievement.Title,
            Description = achievement.Description,
            Points = achievement.Points,
        };
    }

    public static UserAchievementInfo FromDto(this UserAchievementDto achievement, int recademyUserId)
    {
        if (achievement is null)
            return null;

        return new UserAchievementInfo
        {
            UserId = recademyUserId,
            AchievementId = achievement.Id,
        };
    }

    public static UserAchievementRequestDto ToDto(this UserAchievementRequest achievementRequest)
    {
        if (achievementRequest is null)
            return null;

        return new UserAchievementRequestDto
        {
            RequestId = achievementRequest.RequestId,
            UserId = achievementRequest.UserId,
            User = achievementRequest.User.User.ToDto(),
            AchievementId = achievementRequest.AchievementId,
            Achievement = UserAchievementProvider.FindAchievementById(achievementRequest.AchievementId).ToDto(),
            Reason = achievementRequest.Reason,
            RequestTime = achievementRequest.RequestTime,
        };
    }

    public static UserAchievementRequest FromDto(this UserAchievementRequestDto achievementRequest)
    {
        if (achievementRequest is null)
            return null;

        return new UserAchievementRequest
        {
            UserId = achievementRequest.UserId,
            AchievementId = achievementRequest.AchievementId,
            Reason = achievementRequest.Reason,
            RequestTime = achievementRequest.RequestTime,
        };
    }

    public static UserAchievementResponseDto ToDto(this UserAchievementResponse achievementResponse)
    {
        if (achievementResponse is null)
            return null;

        return new UserAchievementResponseDto()
        {
            ResponseId = achievementResponse.ResponseId,
            RequestId = achievementResponse.RequestId,
            Response = achievementResponse.Response.ToDto(),
            Comment = achievementResponse.Comment,
            ResponseTime = achievementResponse.ResponseTime,
        };
    }

    public static UserAchievementResponse FromDto(this UserAchievementResponseDto achievementResponse)
    {
        if (achievementResponse is null)
            return null;

        return new UserAchievementResponse
        {
            ResponseId = achievementResponse.ResponseId,
            RequestId = achievementResponse.RequestId,
            Response = achievementResponse.Response.FromDto(),
            Comment = achievementResponse.Comment,
            ResponseTime = achievementResponse.ResponseTime,
        };
    }

    public static UserAchievementResponseTypeDto ToDto(this UserAchievementResponseType responseType)
    {
        return responseType switch
        {
            UserAchievementResponseType.Approved => UserAchievementResponseTypeDto.Approved,
            UserAchievementResponseType.Declined => UserAchievementResponseTypeDto.Declined,
            UserAchievementResponseType.NoResponse => UserAchievementResponseTypeDto.NoResponse,
            _ => throw new ArgumentOutOfRangeException(nameof(responseType), responseType, null)
        };
    }

    public static UserAchievementResponseType FromDto(this UserAchievementResponseTypeDto responseType)
    {
        return responseType switch
        {
            UserAchievementResponseTypeDto.Approved => UserAchievementResponseType.Approved,
            UserAchievementResponseTypeDto.Declined => UserAchievementResponseType.Declined,
            UserAchievementResponseTypeDto.NoResponse => UserAchievementResponseType.NoResponse,
            _ => throw new ArgumentOutOfRangeException(nameof(responseType), responseType, null)
        };
    }
}