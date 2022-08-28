using System.Collections.Generic;
using Recademy.Shared.Dtos.Projects;

namespace Recademy.Api.Services.Abstraction
{
    public interface IProjectService
    {
        ProjectInfoDto GetProjectInfo(int projectId);
        List<ProjectInfoDto> GetProjectsByTag(string tagName);
        ProjectInfoDto AddProject(AddProjectDto argues);
    }
}