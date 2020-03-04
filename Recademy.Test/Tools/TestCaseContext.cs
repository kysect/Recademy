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
        public readonly UserController UserController;
        public readonly ProjectController ProjectController;
        public readonly ReviewController ReviewController;

        public TestCaseContext()
        {
            RecademyContext context = TestDatabaseProvider.GetDatabaseContext();
            _mocker = new Mocker(context);
            UserController = new UserController(new UserService(context, new AchievementService()));
            ProjectController = new ProjectController(new ProjectService(context));
            ReviewController = new ReviewController(new ReviewService(context));
        }

        public TestCaseContext WithNewUser(out UserInfoDto userInfo) => WithNewUser(out userInfo, UserType.CommonUser);
        public TestCaseContext WithNewMentorUser(out UserInfoDto userInfo) => WithNewUser(out userInfo, UserType.Mentor);
        public TestCaseContext WithNewAdminUser(out UserInfoDto userInfo) => WithNewUser(out userInfo, UserType.Admin);

        public TestCaseContext WithNewUser(out UserInfoDto userInfo, UserType userType)
        {
            User generatedUser = _mocker.GenerateUser(userType);
            userInfo = UserController.ReadUserInfo(generatedUser.Id).Value;

            Assert.NotNull(userInfo);

            return this;
        }

        public TestCaseContext WithNewProjectForUser(UserInfoDto user, out ProjectInfoDto projectInfo)
        {
            return WithNewProjectForUser(user, InstanceFactory.CreateAddProjectDto(user.Id), out projectInfo);
        }

        public TestCaseContext WithNewProjectForUser(UserInfoDto user, AddProjectDto addProjectDto, out ProjectInfoDto projectInfo)
        {
            projectInfo = ProjectController.AddUserProject(addProjectDto).Value;

            Assert.NotNull(projectInfo);

            return this;
        }

        public TestCaseContext WithReviewRequest(ProjectInfoDto projectInfo, out ReviewRequestInfoDto reviewRequest)
        {
            ReviewRequestAddDto reviewRequestAddDto =
                InstanceFactory.CreateReviewRequestAddDto(projectInfo.UserId, projectInfo.ProjectId);
            reviewRequest = ReviewController.CreateReviewRequest(reviewRequestAddDto).Value;

            Assert.NotNull(reviewRequest);

            return this;
        }

        public TestCaseContext CompleteReview(ReviewRequestInfoDto reviewRequest, out ReviewRequestInfoDto result)
        {
            result = ReviewController.CompleteReview(reviewRequest.Id).Value;

            Assert.AreEqual(ProjectState.Completed, result.State);

            return this;
        }

        public TestCaseContext AbandonReview(ReviewRequestInfoDto reviewRequest, out ReviewRequestInfoDto result)
        {
            result = ReviewController.AbandonReview(reviewRequest.Id).Value;

            Assert.AreEqual(ProjectState.Abandoned, result.State);

            return this;
        }
    }
}