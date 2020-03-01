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

        public List<ReviewRequest> GetReviewRequests()
        {
            return _context
                .ReviewRequests
                .Include(s => s.ProjectInfo)
                .ThenInclude(p => p.Skills)
                .Include(s => s.User)
                .Where(s => s.State == ProjectState.Requested)
                .ToList();
        }

        public ReviewRequest AddReviewRequest(int projectId)
        {
            ReviewRequest newRequest = new ReviewRequest
            {
                DateCreate = DateTime.Now,
                ProjectId = projectId,
                State = ProjectState.Requested
            };

            _context.Add(newRequest);
            _context.SaveChanges();

            return newRequest;
        }

        public ReviewResponse SendReviewResponse(SendReviewRequestDto argues)
        {
            ReviewResponse newReview = new ReviewResponse
            {
                ReviewRequestId = argues.ReviewRequestId,
                Description = argues.ReviewText,
                ReviewerId = 1
            };

            _context.ReviewRequests.Find(argues.ReviewRequestId).State = ProjectState.Reviewed;
            _context.ReviewResponses.Add(newReview);
            _context.SaveChanges();

            return newReview;
        }

        public ReviewProjectDto GetReviewInfo(int requestId)
        {
            int projectId = _context
                .ReviewRequests
                .Find(requestId)
                .ProjectId;

            ProjectInfo project = _context
                .ProjectInfos
                .Include(s => s.Skills)
                .FirstOrDefault(s => s.Id == projectId);

            if (project == null)
            {
                throw new RecademyException("No project with current id!");
            }

            return new ReviewProjectDto
            {
                Id = projectId,
                Title = project.Title,
                Link = project.GithubLink
            };
        }


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