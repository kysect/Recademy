using Recademy.Application.Mappings;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Projects;
using Recademy.Core.Models.Reviews;
using Recademy.Core.Tools;
using Recademy.Core.Types;
using Recademy.DataAccess.Repositories.Abstractions;
using Recademy.Dto.Reviews;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Recademy.Application.Services.Implementations;

public class ReviewService : IReviewService
{
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IUserRepository userRepository, IProjectRepository projectRepository, IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
    }

    public IReadOnlyCollection<ReviewRequestInfoDto> GetReviewRequests()
    {
        return _reviewRepository
            .FindActive()
            .To(request => request.ToDto());
    }

    public IReadOnlyCollection<ReviewRequestInfoDto> ReadReviewRequestBySearchContext(ReviewRequestSearchContextDto searchContextDto)
    {
        ArgumentNullException.ThrowIfNull(searchContextDto);

        ICollection<string> currentUserSkills = _userRepository
            .GetUserById(searchContextDto.UserId)
            .UserSkills
            .Select(s => s.SkillName)
            .ToList();

        IReadOnlyCollection<ReviewRequest> result = _reviewRepository.FindActiveByArguments(
            currentUserSkills,
            !searchContextDto.WithoutReviewed,
            searchContextDto.AuthorId,
            searchContextDto.ProjectName,
            searchContextDto.Tags);

        return result.To(request => request.ToDto());
    }

    public ReviewRequestInfoDto AddReviewRequest(ReviewRequestAddDto reviewRequestAddDto)
    {
        ArgumentNullException.ThrowIfNull(reviewRequestAddDto);

        ProjectInfo project = _projectRepository.GetProjectById(reviewRequestAddDto.ProjectId);
        IReadOnlyCollection<ReviewRequest> projectReviewRequest = _reviewRepository.FindForProject(project);
        ReviewRequest lastActiveReview = projectReviewRequest.FirstOrDefault(rr => rr.State == ProjectState.Requested
                                                                         || rr.State == ProjectState.Reviewed);
        if (lastActiveReview != null)
        {
            throw new RecademyException($"Review for this project already exist. Close it before adding new. Review id: {lastActiveReview.Id}");
        }

        //TODO: check if project belong to review author

        var newRequest = new ReviewRequest
        {
            DateCreate = DateTime.UtcNow,
            State = ProjectState.Requested,
            Description = reviewRequestAddDto.Description,
            ProjectId = reviewRequestAddDto.ProjectId,
            UserId = reviewRequestAddDto.UserId,
        };

        return _reviewRepository
            .Create(newRequest)
            .To(request => request.ToDto());
    }

    public ReviewRequestInfoDto CompleteReview(int requestId)
    {
        ReviewRequest reviewRequest = _reviewRepository.GetReviewRequestById(requestId);

        if (reviewRequest.State == ProjectState.Requested)
            throw new RecademyException($"Completing review failed. Review request was not reviewed. Review request id: {reviewRequest.Id}");

        return _reviewRepository
            .UpdateState(reviewRequest, ProjectState.Completed)
            .To(request => request.ToDto());
    }

    public ReviewRequestInfoDto AbandonReview(int requestId)
    {
        ReviewRequest reviewRequest = _reviewRepository.GetReviewRequestById(requestId);

        //TODO: check state
        return _reviewRepository
            .UpdateState(reviewRequest, ProjectState.Abandoned)
            .To(request => request.ToDto());
    }

    public ReviewRequestInfoDto GetReviewInfo(int requestId)
    {
        return _reviewRepository
            .GetReviewRequestById(requestId)
            .To(request => request.ToDto());
    }
}