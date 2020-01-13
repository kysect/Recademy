using Recademy.BlazorWeb.Models;

namespace Recademy.BlazorWeb.Services.Abstraction
{
    public interface IProjectService
    {
        ProjectInfo GetProjectInfo(int projectId);
    }
}