using Recademy.Dto.Reviews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recademy.Application.Services.Abstractions;

public interface IReviewService
{
    Task<IReadOnlyCollection<ReviewRequestInfoDto>> GetReviewRequests();
    Task<IReadOnlyCollection<ReviewRequestInfoDto>> GetReviewRequestsByUserId(int userId);
    ReviewRequestInfoDto GetReviewRequestById(int requestId);
    Task<ReviewRequestInfoDto> CreateReviewRequest(CreateReviewRequestDto createReviewRequestDto);
    Task<ReviewResponseInfoDto> CreateReviewResponse(CreateReviewResponseDto createReviewResponseDto);
    ReviewRequestInfoDto CompleteReview(int requestId);
    ReviewRequestInfoDto AbandonReview(int requestId);
}