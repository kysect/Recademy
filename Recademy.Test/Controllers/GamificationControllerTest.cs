using System.Collections.Generic;
using NUnit.Framework;
using Recademy.Api;
using Recademy.Api.Controllers;
using Recademy.Api.Services;
using Recademy.Mock;
using Recademy.Test.Tools;

namespace Recademy.Test.Controllers
{
    public class GamificationControllerTest
    {
        private RecademyContext _context;
        private Mocker _mocker;
        private UserController _userController;
        private ProjectController _projectController;
        private GamificationController _gamificationController;

        [SetUp]
        public void Setup()
        {
            _context = TestDatabaseProvider.GetDatabaseContext();
            _mocker = new Mocker(_context);
            _userController = new UserController(new UserService(_context, new AchievementService()));
            _projectController = new ProjectController(new ProjectService(_context));
            _gamificationController = new GamificationController(new GamificationService(_context));
        }

        [Test]
        [Ignore("Need to add review response creating")]
        public void AddProject_UserHasProject()
        {
            //TODO: add review response creation
            _gamificationController.CreateReviewUpvote(1, 1);
            List<int> userList = _gamificationController.ReadReviewUpvote(1).Value;

            Assert.Contains(1, userList);
        }
    }
}