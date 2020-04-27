using System.Collections.Generic;
using Recademy.Library.Models;

namespace Recademy.Api.Repositories
{
    public interface IProjectRepository
    {
        ProjectInfo Create(ProjectInfo projectInfo);
        ProjectInfo Find(int id);
        List<ProjectInfo> FindWithTag(string tag);
        ProjectInfo Get(int id);
    }
}