using System.Collections.Generic;
using Recademy.Library.Dto;

namespace Recademy.Api.Services.Abstraction
{
    public interface IReviewService
    {
        List<ReviewRequestInfoDto> GetReviewRequests();
        ReviewRequestInfoDto GetReviewInfo(int requestId);

        ReviewRequestInfoDto AddReviewRequest(ReviewRequestAddDto reviewRequestAddDto);
        ReviewRequestInfoDto SendReviewResponse(int requestId, SendReviewResponseDto reviewResponseDto);
        ReviewRequestInfoDto CompleteReview(int requestId);
        ReviewRequestInfoDto AbandonReview(int requestId);
    }
}