using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models;
using Recademy.Core.Tools;
using Recademy.Core.Types;
using Recademy.DataAccess.Repositories;
using Recademy.Shared.Dtos.Reviews;

namespace Recademy.Application.Services.Implementations
{
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

        public List<ReviewRequestInfoDto> GetReviewRequests()
        {
            return _reviewRepository
                .FindActive()
                .To(rr => new ReviewRequestInfoDto(rr));
        }

        public List<ReviewRequestInfoDto> ReadReviewRequestBySearchContext(ReviewRequestSearchContextDto searchContextDto)
        {
            List<string> currentUserSkills = _userRepository
                .Get(searchContextDto.UserId)
                .UserSkills
                .Select(s => s.SkillName)
                .ToList();

            List<ReviewRequest> result = _reviewRepository.FindActiveByArguments(
                currentUserSkills,
                !searchContextDto.WithoutReviewed,
                searchContextDto.AuthorId,
                searchContextDto.ProjectName,
                searchContextDto.Tags);

            return result.To(rr => new ReviewRequestInfoDto(rr));
        }

        public ReviewRequestInfoDto AddReviewRequest(ReviewRequestAddDto reviewRequestAddDto)
        {
            ProjectInfo project = _projectRepository.Get(reviewRequestAddDto.ProjectId);
            var projectReviewRequest = _reviewRepository.FindForProject(project);
            var lastActiveReview = projectReviewRequest.FirstOrDefault(rr => rr.State == ProjectState.Requested
                                                                             || rr.State == ProjectState.Reviewed);
            if (lastActiveReview != null)
                throw new RecademyException(
                    $"Review for this project already exist. Close it before adding new. Review id: {lastActiveReview.Id}");

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
                .To(rr => new ReviewRequestInfoDto(rr));
        }

        public ReviewRequestInfoDto CompleteReview(int requestId)
        {
            ReviewRequest reviewRequest = _reviewRepository.Get(requestId);
            
            if (reviewRequest.State == ProjectState.Requested)
                throw new RecademyException($"Completing review failed. Review request was not reviewed. Review request id: {reviewRequest.Id}");

            return _reviewRepository
                .UpdateState(reviewRequest, ProjectState.Completed)
                .To(rr => new ReviewRequestInfoDto(rr));
        }

        public ReviewRequestInfoDto AbandonReview(int requestId)
        {
            ReviewRequest reviewRequest = _reviewRepository.Get(requestId);
            
            //TODO: check state
            return _reviewRepository
                .UpdateState(reviewRequest, ProjectState.Abandoned)
                .To(rr => new ReviewRequestInfoDto(rr));
        }

        public ReviewRequestInfoDto GetReviewInfo(int requestId)
        {
            return _reviewRepository
                .Get(requestId)
                .To(rr => new ReviewRequestInfoDto(rr));
        }
    }
}