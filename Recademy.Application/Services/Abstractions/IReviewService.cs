using System.Collections.Generic;
using Recademy.Dto.Reviews;

namespace Recademy.Application.Services.Abstractions
{
    public interface IReviewService
    {
        List<ReviewRequestInfoDto> GetReviewRequests();
        ReviewRequestInfoDto GetReviewInfo(int requestId);
        List<ReviewRequestInfoDto> ReadReviewRequestBySearchContext(ReviewRequestSearchContextDto searchContextDto);

        ReviewRequestInfoDto AddReviewRequest(ReviewRequestAddDto reviewRequestAddDto);
        ReviewRequestInfoDto CompleteReview(int requestId);
        ReviewRequestInfoDto AbandonReview(int requestId);
    }
}