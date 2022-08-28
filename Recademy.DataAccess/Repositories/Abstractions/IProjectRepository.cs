using System.Collections.Generic;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Users;

namespace Recademy.DataAccess.Repositories.Abstractions
{
    public interface IProjectRepository
    {
        ProjectInfo Create(ProjectInfo projectInfo);
        ProjectInfo Find(int id);
        List<ProjectInfo> FindWithTag(string tag);
        ProjectInfo Get(int id);
        List<ProjectInfo> FindByUser(RecademyUser user);
    }
}