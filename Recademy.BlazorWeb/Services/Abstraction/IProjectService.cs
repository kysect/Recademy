using Recademy.Models;

namespace Recademy.Services.Abstraction
{
    public interface IProjectService
    {
        ProjectInfo GetProjectInfo(int projectId);
    }
}