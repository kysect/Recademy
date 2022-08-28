using NUnit.Framework;
using Recademy.Core.Types;
using Recademy.Mock.Generators;
using Recademy.Test.Tools;
using Recademy.Shared.Dtos.Reviews;
using Recademy.Shared.Dtos.Projects;
using Recademy.Shared.Dtos.Users;

namespace Recademy.Test.Controllers
{
    public class ReviewControllerTests
    {
        private TestCaseContext _testContext;

        [SetUp]
        public void InitPerTest()
        {
            _testContext = new TestCaseContext();
        }

        [Test]
        [Ignore("This test requires GitHub credentials. Please run manually for local tests.")]
        public void AddProjectReview_ShouldExist()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewProjectForUser(user, out ProjectInfoDto projectInfo)
                .WithReviewRequest(projectInfo, out ReviewRequestInfoDto _);
        }

        [Test]
        [Ignore("This test requires GitHub credentials. Please run manually for local tests.")]
        public void AddTwoSameProjectReview_ShouldFailWithException()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewProjectForUser(user, out ProjectInfoDto projectInfo)
                .WithReviewRequest(projectInfo, out ReviewRequestInfoDto _);

            ReviewRequestAddDto reviewRequestAddDto =
                InstanceFactory.CreateReviewRequestAddDto(user.Id, projectInfo.ProjectId);
            
            Assert.Catch<RecademyException>(() => _testContext.ReviewController.CreateReviewRequest(reviewRequestAddDto));
        }

        [Test]
        [Ignore("This test requires GitHub credentials. Please run manually for local tests.")]
        public void CompleteReview_Ok()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewProjectForUser(user, out ProjectInfoDto projectInfo)
                .WithReviewRequest(projectInfo, out ReviewRequestInfoDto reviewRequest);

            Assert.Catch<RecademyException>(() => _testContext.ReviewController.CompleteReview(reviewRequest.Id));
        }

        [Test]
        [Ignore("This test requires GitHub credentials. Please run manually for local tests.")]
        public void AbandonReview_Ok()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewProjectForUser(user, out ProjectInfoDto projectInfo)
                .WithReviewRequest(projectInfo, out ReviewRequestInfoDto reviewRequest)
                .AbandonReview(reviewRequest, out _);
        }

        [Test]
        [Ignore("This test requires GitHub credentials. Please run manually for local tests.")]
        public void AddProjectReviewResponse_StateChanged()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewUser(out UserInfoDto otherUser)
                .WithNewProjectForUser(user, out ProjectInfoDto projectInfo)
                .WithReviewRequest(projectInfo, out ReviewRequestInfoDto request)
                .WithReviewResponse(request, otherUser.Id, out ReviewResponseInfoDto reviewResponseInfo);

            Assert.AreEqual(ProjectState.Reviewed, reviewResponseInfo.ReviewRequest.State);
        }
    }
}