using System.Collections.Generic;
using System.Linq;
using Recademy.Application.Mappings;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Skills;
using Recademy.Core.Tools;
using Recademy.DataAccess.Repositories.Abstractions;
using Recademy.Shared.Dtos.Projects;

namespace Recademy.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public ProjectInfoDto GetProjectInfo(int projectId)
        {
            return _projectRepository
                .Get(projectId)
                .To(project => project.ToDto());
        }

        public List<ProjectInfoDto> GetProjectsByTag(string tagName)
        {
            return _projectRepository
                .FindWithTag(tagName)
                .To(project => project.ToDto());
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

            return _projectRepository
                .Create(newProject)
                .To(project => project.ToDto());
        }
    }
}