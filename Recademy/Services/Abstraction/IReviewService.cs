using System.Collections.Generic;
using Recademy.Dto;
using Recademy.Models;

namespace Recademy.Services.Abstraction
{
    public interface IReviewService
    {
        List<ReviewRequest> GetReviewRequests();
        ReviewRequest AddReviewRequest(int ProjectId);
        ReviewResponse SendReviewResponse(SendReviewRequestDto argues);
    }
}