using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Types;
using System.Collections.Generic;

namespace Recademy.DataAccess.Repositories.Abstractions;

public interface IReviewRepository
{
    ReviewRequest Create(ReviewRequest reviewRequest);
    ReviewRequest Find(int id);
    IReadOnlyCollection<ReviewRequest> FindActive();
    IReadOnlyCollection<ReviewRequest> FindActiveByArguments(ICollection<string> userSkills, bool withReviewed, int? authorId, string projectNamePattern, ICollection<string> tags);
    IReadOnlyCollection<ReviewRequest> FindForProject(ProjectInfo project);
    ReviewRequest GetReviewRequestById(int id);
    ReviewRequest UpdateState(ReviewRequest reviewRequest, ProjectState state);
}