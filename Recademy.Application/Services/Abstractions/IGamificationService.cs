using System.Collections.Generic;

namespace Recademy.Application.Services.Abstractions
{
    public interface IGamificationService
    {
        void CreateReviewResponseUpvote(int reviewId, int userId);
        IReadOnlyCollection<int> ReadReviewResponseUpvote(int reviewId);
        void DeleteReviewResponseUpvote(int reviewId, int userId);

        Dictionary<string, int> GetUsersRanking();
        int ReadUserKarmaPointCount(int userId);
    }
}