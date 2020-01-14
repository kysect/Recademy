using Recademy.Library.Models;

namespace Recademy.Api.Services.Abstraction
{
    public interface IProjectService
    {
        ProjectInfo GetProjectInfo(int projectId);
    }
}