using NUnit.Framework;
using Recademy.Api;
using Recademy.Api.Controllers;
using Recademy.Api.Services;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Library.Types;
using Recademy.Mock;
using Recademy.Mock.Generators;
using Recademy.Test.Tools;

namespace Recademy.Test.Controllers
{
    public class ReviewControllerTests
    {
        private RecademyContext _context;
        private Mocker _mocker;
        private ProjectController _projectController;
        private ReviewController _reviewController;

        [SetUp]
        public void Setup()
        {
            _context = TestDatabaseProvider.GetDatabaseContext();
            _mocker = new Mocker(_context);
            _projectController = new ProjectController(new ProjectService(_context));
            _reviewController = new ReviewController(new ReviewService(_context));
        }

        [Test]
        public void AddProjectReview_ShouldExist()
        {
            User studentAccount = _mocker.GenerateUser();
            User mentorAccount = _mocker.GenerateUser();
            AddProjectDto addProjectDto = InstanceFactory.CreateAddProjectDto(studentAccount.Id);

            ProjectInfoDto createdProject = _projectController.AddUserProject(addProjectDto).Value;
            ReviewRequestAddDto reviewRequestAddDto =
                InstanceFactory.CreateReviewRequestAddDto(mentorAccount.Id, createdProject.ProjectId);

            ReviewRequestInfoDto review = _reviewController.CreateReviewRequest(reviewRequestAddDto).Value;

            Assert.NotNull(review);
        }

        [Test]
        public void AddTwoSameProjectReview_ShouldFailWithException()
        {
            User studentAccount = _mocker.GenerateUser();
            AddProjectDto addProjectDto = InstanceFactory.CreateAddProjectDto(studentAccount.Id);

            ProjectInfoDto createdProject = _projectController.AddUserProject(addProjectDto).Value;
            ReviewRequestAddDto reviewRequestAddDto =
                InstanceFactory.CreateReviewRequestAddDto(studentAccount.Id, createdProject.ProjectId);
            _reviewController.CreateReviewRequest(reviewRequestAddDto);
            
            Assert.Catch<RecademyException>(() => _reviewController.CreateReviewRequest(reviewRequestAddDto));
        }

        [Test]
        public void CompleteReview_Ok()
        {
            User studentAccount = _mocker.GenerateUser();
            AddProjectDto addProjectDto = InstanceFactory.CreateAddProjectDto(studentAccount.Id);

            ProjectInfoDto createdProject = _projectController.AddUserProject(addProjectDto).Value;
            ReviewRequestAddDto reviewRequestAddDto = InstanceFactory.CreateReviewRequestAddDto(studentAccount.Id, createdProject.ProjectId);
            ReviewRequestInfoDto requestInfoDto = _reviewController.CreateReviewRequest(reviewRequestAddDto).Value;
            ReviewRequestInfoDto result = _reviewController.CompleteReview(requestInfoDto.Id).Value;

            Assert.AreEqual(ProjectState.Completed, result.State);
        }

        [Test]
        public void AbandonReview_Ok()
        {
            User studentAccount = _mocker.GenerateUser();
            AddProjectDto addProjectDto = InstanceFactory.CreateAddProjectDto(studentAccount.Id);

            ProjectInfoDto createdProject = _projectController.AddUserProject(addProjectDto).Value;
            ReviewRequestAddDto reviewRequestAddDto =
                InstanceFactory.CreateReviewRequestAddDto(studentAccount.Id, createdProject.ProjectId);
            ReviewRequestInfoDto requestInfoDto = _reviewController.CreateReviewRequest(reviewRequestAddDto).Value;
            ReviewRequestInfoDto result = _reviewController.AbandonReview(requestInfoDto.Id).Value;

            Assert.AreEqual(ProjectState.Abandoned, result.State);
        }
    }
}