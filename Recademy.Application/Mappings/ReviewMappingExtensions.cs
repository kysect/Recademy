using Recademy.Core.Models.Reviews;
using Recademy.Core.Tools;
using Recademy.Core.Types;
using Recademy.Dto.Enums;
using Recademy.Dto.Reviews;

using System;

namespace Recademy.Application.Mappings;

public static class ReviewMappingExtensions
{
    public static ReviewRequestInfoDto ToDto(this ReviewRequest reviewRequest)
    {
        if (reviewRequest is null)
            return null;

        return new ReviewRequestInfoDto
        {
            Id = reviewRequest.Id,
            ProjectId = reviewRequest.ProjectId,
            DateCreate = reviewRequest.CreationTime,
            State = reviewRequest.State.ToDto(),
            ProjectInfo = reviewRequest.ProjectInfo.Maybe(project => project.ToDto()),
            User = reviewRequest.User.Maybe(recademyUser => recademyUser.ToDto()),
        };
    }

    public static ReviewRequest FromDto(this ReviewRequestInfoDto reviewRequest)
    {
        if (reviewRequest is null)
            return null;

        return new ReviewRequest
        {
            Id = reviewRequest.Id,
            ProjectId = reviewRequest.ProjectId,
            CreationTime = reviewRequest.DateCreate,
            State = reviewRequest.State.FromDto(),
            ProjectInfo = reviewRequest.ProjectInfo.Maybe(project => project.FromDto()),
            User = reviewRequest.User.Maybe(recademyUser => recademyUser.FromDto()),
        };
    }

    public static ReviewResponseInfoDto ToDto(this ReviewResponse reviewResponse)
    {
        if (reviewResponse is null)
            return null;

        return new ReviewResponseInfoDto
        {
            Id = reviewResponse.Id,
            Description = reviewResponse.Description,
            CreationTime = reviewResponse.CreationTime,
            ReviewConclusion = reviewResponse.ReviewConclusion.ToDto(),
            ReviewRequest = reviewResponse.Request.Maybe(request => request.ToDto()),
            ReviewerId = reviewResponse.ReviewerId,
        };
    }

    public static ReviewResponse FromDto(this ReviewResponseInfoDto reviewResponse)
    {
        if (reviewResponse is null)
            return null;

        return new ReviewResponse
        {
            Id = reviewResponse.Id,
            Description = reviewResponse.Description,
            CreationTime = reviewResponse.CreationTime,
            ReviewConclusion = reviewResponse.ReviewConclusion.FromDto(),
            Request = reviewResponse.ReviewRequest.Maybe(request => request.FromDto()),
            ReviewerId = reviewResponse.ReviewerId,
        };
    }

    public static ReviewResponse FromDto(this CreateReviewResponseDto createReviewResponse)
    {
        if (createReviewResponse is null)
            return null;

        return new ReviewResponse
        {
            RequestId = createReviewResponse.RequestId,
            Description = createReviewResponse.Comment,
            ReviewerId = createReviewResponse.ReviewerId,
            CreationTime = DateTime.UtcNow,
            ReviewConclusion = createReviewResponse.ReviewConclusion.FromDto()
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

    private static ProjectStateDto ToDto(this ReviewState reviewState)
    {
        return reviewState switch
        {
            ReviewState.Requested => ProjectStateDto.Requested,
            ReviewState.Reviewed => ProjectStateDto.Reviewed,
            ReviewState.Completed => ProjectStateDto.Completed,
            ReviewState.Abandoned => ProjectStateDto.Abandoned,
            _ => throw new ArgumentOutOfRangeException(nameof(reviewState), reviewState, null)
        };
    }

    private static ReviewState FromDto(this ProjectStateDto projectState)
    {
        return projectState switch
        {
            ProjectStateDto.Requested => ReviewState.Requested,
            ProjectStateDto.Reviewed => ReviewState.Reviewed,
            ProjectStateDto.Completed => ReviewState.Completed,
            ProjectStateDto.Abandoned => ReviewState.Abandoned,
            _ => throw new ArgumentOutOfRangeException(nameof(projectState), projectState, null)
        };
    }
}