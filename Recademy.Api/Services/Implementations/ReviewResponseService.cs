using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Api.Services.Abstraction;
using Recademy.Core.Models;
using Recademy.Core.Types;
using Recademy.Shared.Dtos.Reviews;

namespace Recademy.Api.Services.Implementations
{
    public class ReviewResponseService : IReviewResponseService
    {
        private readonly RecademyContext _context;

        public ReviewResponseService(RecademyContext context)
        {
            _context = context;
        }

        public ReviewResponseInfoDto SendReviewResponse(ReviewResponseCreateDto reviewResponseCreateDto)
        {
            ReviewRequest request = _context
                .ReviewRequests
                .Include(s => s.ProjectInfo)
                .ThenInclude(p => p.Skills)
                .Include(s => s.User)
                .FirstOrDefault(r => r.Id == reviewResponseCreateDto.ReviewRequestId);

            if (request == null)
                throw RecademyException.ReviewRequestNotFound(reviewResponseCreateDto.ReviewRequestId);

            if (request.State == ProjectState.Completed || request.State == ProjectState.Abandoned)
                throw new RecademyException("Failed to send review. It is already closed.");

            var newReview = new ReviewResponse
            {
                ReviewRequestId = reviewResponseCreateDto.ReviewRequestId,
                Description = reviewResponseCreateDto.ReviewText,
                ReviewerId = reviewResponseCreateDto.UserId,
                CreationTime = DateTime.UtcNow,
                ReviewConclusion = reviewResponseCreateDto.ReviewConclusion
            };

            request.State = ProjectState.Reviewed;
            _context.ReviewResponses.Add(newReview);
            _context.SaveChanges();

            newReview.ReviewRequest = request;
            return new ReviewResponseInfoDto(newReview);
        }
    }
}