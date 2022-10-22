using Recademy.Dto.Projects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recademy.Application.Services.Abstractions;

public interface IProjectService
{
    ProjectInfoDto GetProjectInfo(int projectId);
    IReadOnlyCollection<ProjectInfoDto> GetProjectsByTag(string tagName);
    Task<ProjectInfoDto> CreateProject(CreateProjectDto arguments);
    Task<IReadOnlyCollection<ProjectInfoDto>> GetProjectsByUserId(int userId);
}