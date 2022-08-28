using System.Collections.Generic;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Types;

namespace Recademy.DataAccess.Repositories
{
    public interface IReviewRepository
    {
        ReviewRequest Create(ReviewRequest reviewRequest);
        ReviewRequest Find(int id);
        List<ReviewRequest> FindActive();
        List<ReviewRequest> FindActiveByArguments(List<string> userSkills, bool withReviewed, int? authorId, string projectNamePattern, List<string> tags);
        List<ReviewRequest> FindForProject(ProjectInfo project);
        ReviewRequest Get(int id);
        ReviewRequest UpdateState(ReviewRequest reviewRequest, ProjectState state);
    }
}