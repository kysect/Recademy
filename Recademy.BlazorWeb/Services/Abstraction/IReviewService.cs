using System.Collections.Generic;
using Recademy.BlazorWeb.Dto;
using Recademy.BlazorWeb.Models;

namespace Recademy.BlazorWeb.Services.Abstraction
{
    public interface IReviewService
    {
        List<ReviewRequest> GetReviewRequests();
        ReviewRequest AddReviewRequest(int ProjectId);
        ReviewResponse SendReviewResponse(SendReviewRequestDto argues);
        ReviewProjectDto GetReviewInfo(int requestId);
    }
}