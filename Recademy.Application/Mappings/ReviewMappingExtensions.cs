using System;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Tools;
using Recademy.Core.Types;
using Recademy.Dto.Enums;
using Recademy.Dto.Reviews;

namespace Recademy.Application.Mappings;

public static class ReviewMappingExtensions
{
    public static ReviewRequestInfoDto ToDto(this ReviewRequest reviewRequest)
    {
        return new ReviewRequestInfoDto
        {
            Id = reviewRequest.Id,
            ProjectId = reviewRequest.ProjectId,
            DateCreate = reviewRequest.DateCreate,
            State = reviewRequest.State.ToDto(),
            ProjectInfo = reviewRequest.ProjectInfo.Maybe(project => project.ToDto()),
            User = reviewRequest.User.Maybe(recademyUser => recademyUser.ToDto()),
        };
    }

    public static ReviewRequest FromDto(this ReviewRequestInfoDto reviewRequest)
    {
        return new ReviewRequest
        {
            Id = reviewRequest.Id,
            ProjectId = reviewRequest.ProjectId,
            DateCreate = reviewRequest.DateCreate,
            State = reviewRequest.State.FromDto(),
            ProjectInfo = reviewRequest.ProjectInfo.Maybe(project => project.FromDto()),
            User = reviewRequest.User.Maybe(recademyUser => recademyUser.FromDto()),
        };
    }

    public static ReviewResponseInfoDto ToDto(this ReviewResponse reviewResponse)
    {
        return new ReviewResponseInfoDto
        {
            Id = reviewResponse.Id,
            Description = reviewResponse.Description,
            CreationTime = reviewResponse.CreationTime,
            ReviewConclusion = reviewResponse.ReviewConclusion.ToDto(),
            ReviewRequest = reviewResponse.ReviewRequest.Maybe(request => request.ToDto()),
            ReviewerId = reviewResponse.ReviewerId,
        };
    }

    public static ReviewResponse FromDto(this ReviewResponseCreateDto reviewResponse)
    {
        return new ReviewResponse
        {
            ReviewRequestId = reviewResponse.ReviewRequestId,
            Description = reviewResponse.ReviewText,
            ReviewerId = reviewResponse.UserId,
            CreationTime = DateTime.UtcNow,
            ReviewConclusion = reviewResponse.ReviewConclusion.FromDto()
        };
    }

    public static ReviewResponse FromDto(this ReviewResponseInfoDto reviewResponse)
    {
        return new ReviewResponse
        {
            Id = reviewResponse.Id,
            Description = reviewResponse.Description,
            CreationTime = reviewResponse.CreationTime,
            ReviewConclusion = reviewResponse.ReviewConclusion.FromDto(),
            ReviewRequest = reviewResponse.ReviewRequest.Maybe(request => request.FromDto()),
            ReviewerId = reviewResponse.ReviewerId,
        };
    }

    private static ReviewConclusionDto ToDto(this ReviewConclusion reviewConclusion)
    {
        return reviewConclusion switch
        {
            ReviewConclusion.LooksGood => ReviewConclusionDto.LooksGood,
            ReviewConclusion.WithComments => ReviewConclusionDto.WithComments,
            ReviewConclusion.NeedWork => ReviewConclusionDto.NeedWork,
            _ => throw new ArgumentOutOfRangeException(nameof(reviewConclusion), reviewConclusion, null)
        };
    }

    private static ReviewConclusion FromDto(this ReviewConclusionDto reviewConclusion)
    {
        return reviewConclusion switch
        {
            ReviewConclusionDto.LooksGood => ReviewConclusion.LooksGood,
            ReviewConclusionDto.WithComments => ReviewConclusion.WithComments,
            ReviewConclusionDto.NeedWork => ReviewConclusion.NeedWork,
            _ => throw new ArgumentOutOfRangeException(nameof(reviewConclusion), reviewConclusion, null)
        };
    }

    private static ProjectStateDto ToDto(this ProjectState projectState)
    {
        return projectState switch
        {
            ProjectState.Requested => ProjectStateDto.Requested,
            ProjectState.Reviewed => ProjectStateDto.Reviewed,
            ProjectState.Completed => ProjectStateDto.Completed,
            ProjectState.Abandoned => ProjectStateDto.Abandoned,
            _ => throw new ArgumentOutOfRangeException(nameof(projectState), projectState, null)
        };
    }

    private static ProjectState FromDto(this ProjectStateDto projectState)
    {
        return projectState switch
        {
            ProjectStateDto.Requested => ProjectState.Requested,
            ProjectStateDto.Reviewed => ProjectState.Reviewed,
            ProjectStateDto.Completed => ProjectState.Completed,
            ProjectStateDto.Abandoned => ProjectState.Abandoned,
            _ => throw new ArgumentOutOfRangeException(nameof(projectState), projectState, null)
        };
    }
}