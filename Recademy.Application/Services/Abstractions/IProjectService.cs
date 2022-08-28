using Recademy.Dto.Projects;

using System.Collections.Generic;

namespace Recademy.Application.Services.Abstractions
{
    public interface IProjectService
    {
        ProjectInfoDto GetProjectInfo(int projectId);
        List<ProjectInfoDto> GetProjectsByTag(string tagName);
        ProjectInfoDto AddProject(AddProjectDto argues);
    }
}