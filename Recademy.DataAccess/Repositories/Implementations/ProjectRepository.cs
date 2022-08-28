using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Core.Models;
using Recademy.Core.Types;

namespace Recademy.DataAccess.Repositories.Implementations
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly RecademyContext _context;

        public ProjectRepository(RecademyContext context)
        {
            _context = context;
        }

        public ProjectInfo Create(ProjectInfo projectInfo)
        {
            _context.ProjectInfos.Add(projectInfo);
            _context.SaveChanges();
            return projectInfo;
        }

        public ProjectInfo Find(int id)
        {
            return _context
                .ProjectInfos
                .Include(s => s.Skills)
                .FirstOrDefault(k => k.Id == id);
        }

        public ProjectInfo Get(int id)
        {
            return Find(id) ?? throw RecademyException.ProjectNotFound(id);
        }

        public List<ProjectInfo> FindWithTag(string tag)
        {
            return _context
                .ProjectInfos
                .Include(p => p.Skills)
                .Where(p => p
                    .Skills
                    .Any(s => s.SkillName == tag))
                .ToList();
        }

        public List<ProjectInfo> FindByUser(User user)
        {
            return _context.ProjectInfos
                .Include(p => p.Skills)
                .Include(p => p.ReviewRequests)
                .Where(p => p.AuthorId == user.Id)
                .ToList();
        }
    }
}