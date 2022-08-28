using Recademy.Dto.Projects;

using System.Collections.Generic;

namespace Recademy.Application.Services.Abstractions
{
    public interface IProjectService
    {
        ProjectInfoDto GetProjectInfo(int projectId);
        IReadOnlyCollection<ProjectInfoDto> GetProjectsByTag(string tagName);
        ProjectInfoDto AddProject(AddProjectDto arguments);
    }
}