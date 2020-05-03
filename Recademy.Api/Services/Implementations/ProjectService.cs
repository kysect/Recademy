using System.Collections.Generic;
using System.Linq;
using Recademy.Api.Repositories;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Library.Tools;

namespace Recademy.Api.Services.Implementations
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
                .To(p => new ProjectInfoDto(p));
        }

        public List<ProjectInfoDto> GetProjectsByTag(string tagName)
        {
            return _projectRepository
                .FindWithTag(tagName)
                .To(p => new ProjectInfoDto(p));
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
                .To(p => new ProjectInfoDto(p));
        }
    }
}