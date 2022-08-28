using Recademy.Application.Mappings;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Skills;
using Recademy.Core.Tools;
using Recademy.DataAccess.Repositories.Abstractions;
using Recademy.Dto.Projects;

using System;
using System.Collections.Generic;
using System.Linq;

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

        public IReadOnlyCollection<ProjectInfoDto> GetProjectsByTag(string tagName)
        {
            return _projectRepository
                .FindWithTag(tagName)
                .To(project => project.ToDto());
        }

        public ProjectInfoDto AddProject(AddProjectDto arguments)
        {
            ArgumentNullException.ThrowIfNull(arguments);

            var newProject = new ProjectInfo
            {
                AuthorId = arguments.UserId,
                GithubLink = arguments.ProjectUrl,
                Title = arguments.ProjectName,
                Skills = arguments
                    .Tags
                    .Select(t => new ProjectSkill { SkillName = t })
                    .ToList()
            };

            return _projectRepository
                .Create(newProject)
                .To(project => project.ToDto());
        }
    }
}