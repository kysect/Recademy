using System.Collections.Generic;

namespace Recademy.Api.Services.Abstraction
{
    public interface IGamificationService
    {
        void CreateReviewResponseUpvote(int reviewId, int userId);
        List<int> ReadReviewResponseUpvote(int reviewId);
        void DeleteReviewResponseUpvote(int reviewId, int userId);

        Dictionary<string, int> GetUsersRanking();
    }
}