using System.Collections.Generic;
using Recademy.Library.Dto;
using Recademy.Library.Models;

namespace Recademy.Api.Services.Abstraction
{
    public interface IReviewService
    {
        List<ReviewRequest> GetReviewRequests();
        ReviewRequest AddReviewRequest(int ProjectId);
        ReviewResponse SendReviewResponse(SendReviewRequestDto argues);
        ReviewProjectDto GetReviewInfo(int requestId);
    }
}