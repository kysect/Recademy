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
    public class ProjectControllerTests
    {
        private RecademyContext _context;
        private Mocker _mocker;
        private UserController _userController;
        private ProjectController _projectController;

        [SetUp]
        public void Setup()
        {
            _context = TestDatabaseProvider.GetDatabaseContext();
            _mocker = new Mocker(_context);
            _userController = new UserController(new UserService(_context, new AchievementService()));
            _projectController = new ProjectController(new ProjectService(_context));
        }

        [Test]
        public void AddProject_UserHasProject()
        {
            User generatedUser = _mocker.GenerateUser();
            AddProjectDto addProjectDto = InstanceFactory.CreateAddProjectDto(generatedUser.Id);

            ProjectInfoDto createdProject = _projectController.AddUserProject(addProjectDto).Value;
            UserInfoDto createdUser = _userController.GetUserInfo(generatedUser.Id).Value;

            Assert.NotNull(createdUser);
            Assert.NotNull(createdProject);

            ProjectInfoDto project = createdUser.ProjectDtos.Find(p => p.ProjectId == createdProject.ProjectId);
            Assert.NotNull(project);
        }
    }
}