using Recademy.Core.Models.Reviews;
using Recademy.Core.Tools;
using Recademy.Shared.Dtos.Reviews;

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
            State = reviewRequest.State,
            ProjectInfo = reviewRequest.ProjectInfo.Maybe(project => project.ToDto()),
            User = reviewRequest.User.Maybe(recademyUser => recademyUser.ToDto()),
        };
    }

    public static ReviewResponseInfoDto ToDto(this ReviewResponse reviewResponse)
    {
        return new ReviewResponseInfoDto
        {
            Id = reviewResponse.Id,
            Description = reviewResponse.Description,
            CreationTime = reviewResponse.CreationTime,
            ReviewConclusion = reviewResponse.ReviewConclusion,
            ReviewRequest = reviewResponse.ReviewRequest.Maybe(request => request.ToDto()),
            ReviewerId = reviewResponse.ReviewerId,
    };
    }
}