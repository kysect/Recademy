using Microsoft.EntityFrameworkCore;

using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Types;
using Recademy.DataAccess.Repositories.Abstractions;

using System.Collections.Generic;
using System.Linq;

namespace Recademy.DataAccess.Repositories.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RecademyContext _context;

        public ReviewRepository(RecademyContext context)
        {
            _context = context;
        }

        public ReviewRequest Create(ReviewRequest reviewRequest)
        {
            _context.Add(reviewRequest);
            _context.SaveChanges();
            return reviewRequest;
        }

        public ReviewRequest Find(int id)
        {
            return _context
                .ReviewRequests
                .Include(s => s.ProjectInfo)
                .ThenInclude(p => p.Skills)
                .Include(s => s.User)
                .FirstOrDefault(r => r.Id == id);
        }

        public ReviewRequest Get(int id)
        {
            return _context.ReviewRequests.Find(id) ?? throw RecademyException.ReviewRequestNotFound(id);
        }

        public List<ReviewRequest> FindActive()
        {
            return _context
                .ReviewRequests
                .Include(s => s.ProjectInfo)
                .ThenInclude(p => p.Skills)
                .Include(s => s.User)
                .Where(s => s.State == ProjectState.Requested)
                .ToList();
        }

        public List<ReviewRequest> FindForProject(ProjectInfo project)
        {
            return _context.ReviewRequests
                .Where(r => r.ProjectId == project.Id)
                .ToList();
        }

        public List<ReviewRequest> FindActiveByArguments(
            List<string> userSkills,
            bool withReviewed,
            int? authorId,
            string projectNamePattern,
            List<string> tags)
        {
            IQueryable<ReviewRequest> query = _context
                .ReviewRequests
                .Include(s => s.ProjectInfo)
                .ThenInclude(p => p.Skills)
                .Include(s => s.User)
                .Where(s => s.State == ProjectState.Requested || s.State == ProjectState.Reviewed && withReviewed)
                .Where(r => r.ProjectInfo.Skills.All(s => userSkills.Contains(s.SkillName)));

            if (authorId != null)
                query = query.Where(r => r.UserId == authorId);

            if (projectNamePattern != null)
                query = query.Where(r => r.ProjectInfo.Title.Contains(projectNamePattern));

            if (tags != null && tags.Any())
                query = query.Where(r => r.ProjectInfo.Skills.Any(s => tags.Contains(s.SkillName)));

            return query.ToList();
        }

        public ReviewRequest UpdateState(ReviewRequest reviewRequest, ProjectState state)
        {
            reviewRequest.State = ProjectState.Abandoned;
            _context.SaveChanges();
            return reviewRequest;
        }
    }
}