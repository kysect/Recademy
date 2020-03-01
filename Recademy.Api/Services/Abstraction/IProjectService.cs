using System.Collections.Generic;
using Recademy.Library.Dto;
using Recademy.Library.Models;

namespace Recademy.Api.Services.Abstraction
{
    public interface IProjectService
    {
        ProjectInfo GetProjectInfo(int projectId);
        List<ProjectDto> GetProjectsByTag(string tagName);
    }
}