using System.Collections.Generic;
using System.Linq;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Models;
using Recademy.Library.Types;

namespace Recademy.Api.Services
{
    public class GamificationService : IGamificationService
    {
        private RecademyContext _context;

        public GamificationService(RecademyContext context)
        {
            _context = context;
        }

        public void CreateReviewResponseUpvote(int reviewId, int userId)
        {
            ReviewResponse review = _context.ReviewResponses.Find(reviewId);
            if (review == null)
                throw RecademyException.ReviewRequestNotFound(reviewId);

            if (review.ReviewerId == userId)
                throw new RecademyException("Try to upvote self review response");

            _context.ReviewResponseUpvotes.Add(new ReviewResponseUpvote {ReviewResponseId = reviewId, UserId = userId});
            _context.SaveChanges();
        }

        public List<int> ReadReviewResponseUpvote(int reviewId)
        {
            return _context
                .ReviewResponseUpvotes
                .Where(u => u.ReviewResponseId == reviewId)
                .Select(ru => ru.UserId)
                .ToList();
        }

        public void DeleteReviewResponseUpvote(int reviewId, int userId)
        {
            _context.ReviewResponseUpvotes.Remove(new ReviewResponseUpvote { ReviewResponseId = reviewId, UserId = userId });
            _context.SaveChanges();
        }
    }
}