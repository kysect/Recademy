using Microsoft.EntityFrameworkCore;

using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Types;
using Recademy.DataAccess;

using System.Collections.Generic;
using System.Linq;

namespace Recademy.Application.Services.Implementations;

public class GamificationService : IGamificationService
{
    private readonly RecademyContext _context;

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

        _context.ReviewResponseUpvotes.Add(new ReviewResponseUpvote { ReviewResponseId = reviewId, UserId = userId });
        _context.SaveChanges();
    }

    public IReadOnlyCollection<int> ReadReviewResponseUpvote(int reviewId)
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

    public Dictionary<string, int> GetUsersRanking()
    {
        return _context
            .Users
            .ToList()
            .Select(u => (u.Name, Points: GetUserKarmaPointCount(u.Id)))
            .OrderByDescending(t => t.Points)
            .ToDictionary(t => t.Name, t => t.Points);
    }

    public int GetUserKarmaPointCount(int userId)
    {
        int upvotes = _context
            .ReviewResponseUpvotes
            .Include(u => u.ReviewResponse)
            .Count(u => u.ReviewResponse.ReviewerId == userId);

        int projectCount = _context
            .ProjectInfos
            .Count(p => p.AuthorId == userId);

        int reviewReponsesCount = _context
            .ReviewResponses
            .Count(r => r.ReviewerId == userId);

        return (5 * reviewReponsesCount) + (3 * projectCount) + upvotes;
    }
}