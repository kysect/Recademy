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
                .Where(s => s.State == ProjectState.Requested)
                .Select(m => new ReviewRequestInfoDto(m))
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

        //TODO: check this methods
        private bool IsValid(List<string> projectSkills, List<string> tags)
        {
            return projectSkills.Any(tags.Contains);
        }

        public List<ReviewRequest> GetReviewRequestsForUser(int userId)
        {
            var tags = _context
                .Users
                .Find(userId)
                .UserSkills
                .Select(s => s.SkillName)
                .ToList();

            return _context
                .ReviewRequests
                .Where(s => s.State == ProjectState.Requested)
                .Where(s =>
                    IsValid(s
                        .ProjectInfo
                        .Skills
                        .Select(t => t.SkillName)
                        .ToList(), tags))
                .ToList();
        }

        public List<ReviewRequest> GetRequestsByFilter(GetRequestsByFilterDto argues)
        {
            return _context
                .ReviewRequests
                .Where(s => s.State == ProjectState.Requested)
                .Where(s =>
                    IsValid(s
                        .ProjectInfo
                        .Skills
                        .Select(t => t.SkillName)
                        .ToList(), argues.Tags))
                .ToList();
        }
    }
}