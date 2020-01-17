using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Library.Types;

namespace Recademy.Api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly RecademyContext _context;

        public ProjectService(RecademyContext context)
        {
            _context = context;
        }

        public ProjectInfo GetProjectInfo(int projectId)
        {
            var project = _context
                .ProjectInfos
                .Include(s => s.Skills)
                .FirstOrDefault(k => k.Id == projectId);

            if (project == null)
            {
                throw new RecademyException("No project with current id!");
            }

            return project;
        }

        public List<ProjectDto> GetProjectsByTag(string tagName)
        {
            var projects = _context
                .ProjectInfos
                .Include(p => p.Skills)
                .Where(p => p
                    .Skills
                    .Any(s => s.SkillName == tagName))
                .ToList();

            return projects
                .Select(k => new ProjectDto(k))
                .ToList();
        }
    }
}