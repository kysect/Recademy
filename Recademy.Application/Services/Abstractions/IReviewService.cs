using Recademy.Dto.Reviews;

using System.Collections.Generic;

namespace Recademy.Application.Services.Abstractions;

public interface IReviewService
{
    IReadOnlyCollection<ReviewRequestInfoDto> GetReviewRequests();
    ReviewRequestInfoDto GetReviewInfo(int requestId);
    IReadOnlyCollection<ReviewRequestInfoDto> ReadReviewRequestBySearchContext(ReviewRequestSearchContextDto searchContextDto);

    ReviewRequestInfoDto AddReviewRequest(ReviewRequestAddDto reviewRequestAddDto);
    ReviewRequestInfoDto CompleteReview(int requestId);
    ReviewRequestInfoDto AbandonReview(int requestId);
}