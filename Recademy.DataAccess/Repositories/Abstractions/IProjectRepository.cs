using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Users;

using System.Collections.Generic;

namespace Recademy.DataAccess.Repositories.Abstractions;

public interface IProjectRepository
{
    ProjectInfo Create(ProjectInfo projectInfo);
    ProjectInfo Find(int id);
    IReadOnlyCollection<ProjectInfo> FindWithTag(string tag);
    ProjectInfo GetProjectById(int id);
    IReadOnlyCollection<ProjectInfo> FindByUser(RecademyUser user);
}