using Recademy.Models;

namespace Recademy.Services.Abstraction
{
    public interface IProjectService
    {
        public ProjectInfo GetProjectInfo(int projectId);
    }
}