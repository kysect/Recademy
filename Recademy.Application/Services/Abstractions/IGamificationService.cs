namespace Recademy.Application.Services.Abstractions
{
    public interface IGamificationService
    {
        void CreateReviewResponseUpvote(int reviewId, int userId);
        List<int> ReadReviewResponseUpvote(int reviewId);
        void DeleteReviewResponseUpvote(int reviewId, int userId);

        Dictionary<string, int> GetUsersRanking();
        int ReadUserKarmaPointCount(int userId);
    }
}