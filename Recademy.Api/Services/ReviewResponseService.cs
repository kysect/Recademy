using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Library.Types;

namespace Recademy.Api.Services
{
    public class ReviewResponseService : IReviewResponseService
    {
        private readonly RecademyContext _context;

        public ReviewResponseService(RecademyContext context)
        {
            _context = context;
        }

        public ReviewRequestInfoDto SendReviewResponse(SendReviewResponseDto reviewResponseDto)
        {
            ReviewRequest request = _context
                .ReviewRequests
                .Include(s => s.ProjectInfo)
                .ThenInclude(p => p.Skills)
                .Include(s => s.User)
                .FirstOrDefault(r => r.Id == reviewResponseDto.ReviewRequestId);

            if (request == null)
                throw RecademyException.ReviewRequestNotFound(reviewResponseDto.ReviewRequestId);

            var newReview = new ReviewResponse
            {
                ReviewRequestId = reviewResponseDto.ReviewRequestId,
                Description = reviewResponseDto.ReviewText,
                ReviewerId = reviewResponseDto.UserId
            };

            //TODO: check state
            request.State = ProjectState.Reviewed;
            _context.ReviewResponses.Add(newReview);
            _context.SaveChanges();

            //TODO: replace with review response
            return new ReviewRequestInfoDto(request);
        }
    }
}