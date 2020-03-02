using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Recademy.Api;
using Recademy.Api.Controllers;
using Recademy.Api.Services;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Mock;
using Recademy.Mock.Generators;
using Recademy.Test.Tools;

namespace Recademy.Test.Controllers
{
    public class ReviewControllerTests
    {
        private RecademyContext _context;
        private Mocker _mocker;
        private UserController _userController;
        private ProjectController _projectController;
        private ReviewController _reviewController;

        [SetUp]
        public void Setup()
        {
            _context = TestDatabaseProvider.GetDatabaseContext();
            _mocker = new Mocker(_context);
            _userController = new UserController(new UserService(_context, new AchievementService()));
            _projectController = new ProjectController(new ProjectService(_context));
            _reviewController = new ReviewController(new ReviewService(_context));
        }

        [Test]
        public void AddProject_UserHasProject()
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
    }
}