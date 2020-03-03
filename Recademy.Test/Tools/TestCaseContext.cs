using NUnit.Framework;
using Recademy.Api;
using Recademy.Api.Controllers;
using Recademy.Api.Services;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Library.Types;
using Recademy.Mock;
using Recademy.Mock.Generators;

namespace Recademy.Test.Tools
{
    public class TestCaseContext
    {
        private readonly Mocker _mocker;
        private readonly UserController _userController;
        private readonly ProjectController _projectController;
        private readonly ReviewController _reviewController;

        public TestCaseContext()
        {
            RecademyContext context = TestDatabaseProvider.GetDatabaseContext();
            _mocker = new Mocker(context);
            _userController = new UserController(new UserService(context, new AchievementService()));
            _projectController = new ProjectController(new ProjectService(context));
            _reviewController = new ReviewController(new ReviewService(context));
        }

        public TestCaseContext WithNewUser(out UserInfoDto userInfo)
        {
            User generatedUser = _mocker.GenerateUser();
            userInfo = _userController.GetUserInfo(generatedUser.Id).Value;

            Assert.NotNull(userInfo);

            return this;
        }

        public TestCaseContext WithNewProjectForUser(UserInfoDto user, out ProjectInfoDto projectInfo)
        {
            return WithNewProjectForUser(user, InstanceFactory.CreateAddProjectDto(user.Id), out projectInfo);
        }

        public TestCaseContext WithNewProjectForUser(UserInfoDto user, AddProjectDto addProjectDto, out ProjectInfoDto projectInfo)
        {
            projectInfo = _projectController.AddUserProject(addProjectDto).Value;

            Assert.NotNull(projectInfo);

            return this;
        }

        public TestCaseContext WithReviewRequest(ProjectInfoDto projectInfo, out ReviewRequestInfoDto reviewRequest)
        {
            ReviewRequestAddDto reviewRequestAddDto =
                InstanceFactory.CreateReviewRequestAddDto(projectInfo.UserId, projectInfo.ProjectId);
            reviewRequest = _reviewController.CreateReviewRequest(reviewRequestAddDto).Value;

            Assert.NotNull(reviewRequest);

            return this;
        }

        public TestCaseContext CompleteReview(ReviewRequestInfoDto reviewRequest, out ReviewRequestInfoDto result)
        {
            result = _reviewController.CompleteReview(reviewRequest.Id).Value;

            Assert.AreEqual(ProjectState.Completed, result.State);

            return this;
        }

        public TestCaseContext AbandonReview(ReviewRequestInfoDto reviewRequest, out ReviewRequestInfoDto result)
        {
            result = _reviewController.AbandonReview(reviewRequest.Id).Value;

            Assert.AreEqual(ProjectState.Abandoned, result.State);

            return this;
        }
    }
}