using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Library.Types;

namespace Recademy.Api.Services
{
    public class ReviewService : IReviewService
    {
        private readonly RecademyContext _context;

        public ReviewService(RecademyContext context)
        {
            _context = context;
        }

        public List<ReviewRequestInfoDto> GetReviewRequests()
        {
            return _context
                .ReviewRequests
                .Include(s => s.ProjectInfo)
                .ThenInclude(p => p.Skills)
                .Include(s => s.User)
                .Where(s => s.State == ProjectState.Requested || s.State == ProjectState.Requested)
                .Select(m => new ReviewRequestInfoDto(m))
                .ToList();
        }

        public List<ReviewRequestInfoDto> ReadReviewRequestBySearchContext(ReviewRequestSearchContextDto searchContextDto)
        {
            User user = _context.Users
                .Include(u => u.UserSkills)
                .FirstOrDefault(u => u.Id == searchContextDto.UserId);

            if (user == null)
                throw RecademyException.UserNotFound(searchContextDto.UserId);

            List<String> userSkills = user.UserSkills
                .Select(s => s.SkillName)
                .ToList();

            IQueryable<ReviewRequest> query = _context
                .ReviewRequests
                .Include(s => s.ProjectInfo)
                .ThenInclude(p => p.Skills)
                .Include(s => s.User)
                .Where(s => s.State == ProjectState.Requested || s.State == ProjectState.Reviewed && !searchContextDto.WithoutReviewed)
                .Where(r => r.ProjectInfo.Skills.All(s => userSkills.Contains(s.SkillName)));

            if (searchContextDto?.AuthorId != null)
                query = query.Where(r => r.UserId == searchContextDto.AuthorId.Value);

            if (searchContextDto?.ProjectName != null)
                query = query.Where(r => r.ProjectInfo.Title.Contains(searchContextDto.ProjectName));

            if (searchContextDto?.Tags != null)
                query = query.Where(r => r.ProjectInfo.Skills.Any(s => searchContextDto.Tags.Contains(s.SkillName)));

            return query
                .Select(r => new ReviewRequestInfoDto(r))
                .ToList();
        }

        public ReviewRequestInfoDto AddReviewRequest(ReviewRequestAddDto reviewRequestAddDto)
        {
            CheckForNotFinishedReview(reviewRequestAddDto.ProjectId);

            //TODO: check if project belong to review author

            var newRequest = new ReviewRequest
            {
                DateCreate = DateTime.UtcNow,
                State = ProjectState.Requested,
                Description = reviewRequestAddDto.Description,
                ProjectId = reviewRequestAddDto.ProjectId,
                UserId = reviewRequestAddDto.UserId,
            };

            _context.Add(newRequest);
            _context.SaveChanges();

            return new ReviewRequestInfoDto(newRequest);
        }

        public ReviewRequestInfoDto SendReviewResponse(int requestId, SendReviewResponseDto reviewResponseDto)
        {
            ReviewRequest request = _context
                .ReviewRequests
                .Include(s => s.ProjectInfo)
                .ThenInclude(p => p.Skills)
                .Include(s => s.User)
                .FirstOrDefault(r => r.Id == requestId);

            if (request == null)
                throw RecademyException.ReviewRequestNotFound(requestId);

            var newReview = new ReviewResponse
            {
                ReviewRequestId = requestId,
                Description = reviewResponseDto.ReviewText,
                ReviewerId = reviewResponseDto.UserId
            };

            request.State = ProjectState.Reviewed;
            _context.ReviewResponses.Add(newReview);
            _context.SaveChanges();

            return new ReviewRequestInfoDto(request);
        }

        public ReviewRequestInfoDto CompleteReview(int requestId)
        {
            ReviewRequest request = _context.ReviewRequests.Find(requestId) ?? throw RecademyException.ReviewRequestNotFound(requestId);
            
            if (request.State == ProjectState.Requested)
                throw new RecademyException($"Completing review failed. Review request was not reviewed. Review request id: {request}");

            request.State = ProjectState.Completed;
            _context.SaveChanges();

            return new ReviewRequestInfoDto(request);
        }

        public ReviewRequestInfoDto AbandonReview(int requestId)
        {
            ReviewRequest request = _context.ReviewRequests.Find(requestId) ?? throw RecademyException.ReviewRequestNotFound(requestId);
            request.State = ProjectState.Abandoned;
            _context.SaveChanges();

            return new ReviewRequestInfoDto(request);
        }

        public ReviewRequestInfoDto GetReviewInfo(int requestId)
        {
            ReviewRequest request = _context
                .ReviewRequests
                .Include(s => s.ProjectInfo)
                .ThenInclude(p => p.Skills)
                .Include(s => s.User)
                .FirstOrDefault(r => r.Id == requestId);

            return new ReviewRequestInfoDto(request);
        }

        private void CheckForNotFinishedReview(int projectId)
        {
            ReviewRequest previousReview = _context.ReviewRequests
                .Where(r => r.ProjectId == projectId)
                .FirstOrDefault(r => r.State == ProjectState.Requested || r.State == ProjectState.Reviewed);

            if (previousReview != null)
                throw new RecademyException(
                    $"Review for this project already exist. Close it before adding new. Review id: {previousReview.Id}");
        }
    }
}