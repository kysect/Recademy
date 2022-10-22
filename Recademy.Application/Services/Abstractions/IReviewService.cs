using Recademy.Dto.Reviews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recademy.Application.Services.Abstractions;

public interface IReviewService
{
    Task<IReadOnlyCollection<ReviewRequestInfoDto>> GetReviewRequests();
    ReviewRequestInfoDto GetReviewRequestById(int requestId);
    ReviewRequestInfoDto CreateReviewRequest(CreateReviewRequestDto createReviewRequestDto);
    ReviewResponseInfoDto CreateReviewResponse(CreateReviewResponseDto createReviewResponseDto);
    ReviewRequestInfoDto CompleteReview(int requestId);
    ReviewRequestInfoDto AbandonReview(int requestId);
}