namespace Recademy.Api.Services.Abstraction
{
    public interface IGamificationService
    {
        void CreateReviewResponseUpvote(int reviewId, int userId);
        void ReadReviewResponseUpvote(int reviewId);
        void DeleteReviewResponseUpvote(int reviewId, int userId);
    }
}