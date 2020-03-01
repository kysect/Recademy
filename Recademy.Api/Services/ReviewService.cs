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
            var newRequest = new ReviewRequest
            {
                ProjectId = reviewRequestAddDto.ProjectId,
                UserId = reviewRequestAddDto.UserId,
                DateCreate = DateTime.UtcNow,
                State = ProjectState.Requested
            };

            _context.Add(newRequest);
            _context.SaveChanges();

            return new ReviewRequestInfoDto(newRequest);
        }

        public ReviewRequestInfoDto SendReviewResponse(SendReviewResponseDto reviewResponseDto)
        {
            ReviewRequest request = _context
                .ReviewRequests
                .Include(s => s.ProjectInfo)
                .ThenInclude(p => p.Skills)
                .Include(s => s.User)
                .FirstOrDefault(r => r.Id == reviewResponseDto.Id);

            if (request == null)
                throw RecademyException.ReviewRequestNotFound(reviewResponseDto.Id);

            var newReview = new ReviewResponse
            {
                ReviewRequestId = reviewResponseDto.Id,
                Description = reviewResponseDto.ReviewText,
                ReviewerId = reviewResponseDto.UserId
            };

            request.State = ProjectState.Reviewed;
            _context.ReviewResponses.Add(newReview);
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