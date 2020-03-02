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

        public ProjectInfoDto GetProjectInfo(int projectId)
        {
            ProjectInfo project = _context
                .ProjectInfos
                .Include(s => s.Skills)
                .FirstOrDefault(k => k.Id == projectId);

            if (project == null)
                throw RecademyException.ProjectNotFound(projectId);

            return new ProjectInfoDto(project);
        }

        public List<ProjectInfoDto> GetProjectsByTag(string tagName)
        {
            return _context
                .ProjectInfos
                .Include(p => p.Skills)
                .Where(p => p
                    .Skills
                    .Any(s => s.SkillName == tagName))
                .Select(k => new ProjectInfoDto(k))
                .ToList();
        }

        public ProjectInfoDto AddProject(AddProjectDto argues)
        {
            var newProject = new ProjectInfo
            {
                AuthorId = argues.UserId,
                GithubLink = argues.ProjectUrl,
                Title = argues.ProjectName,
                Skills = argues
                    .Tags
                    .Select(t => new ProjectSkill {SkillName = t})
                    .ToList()
            };

            _context.ProjectInfos.Add(newProject);
            _context.SaveChanges();

            return new ProjectInfoDto(newProject);
        }
    }
}