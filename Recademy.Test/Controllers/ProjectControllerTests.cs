using NUnit.Framework;
using Recademy.Api;
using Recademy.Api.Controllers;
using Recademy.Api.Services;
using Recademy.Library.Dto;
using Recademy.Mock.Generators;
using Recademy.Test.Tools;

namespace Recademy.Test.Controllers
{
    public class ProjectControllerTests
    {
        private RecademyContext _context;
        private UserController _userController;
        private readonly TestCaseContext _testContext = new TestCaseContext();

        [SetUp]
        public void Setup()
        {
            _context = TestDatabaseProvider.GetDatabaseContext();
            _userController = new UserController(new UserService(_context, new AchievementService()));
        }

        [Test]
        public void AddProject_UserHasProject()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewProjectForUser(user, out ProjectInfoDto projectInfo);
            UserInfoDto createdUser = _userController.GetUserInfo(user.Id).Value;
            //TODO: add method for getting user projects
            ProjectInfoDto project = createdUser.ProjectDtos.Find(p => p.ProjectId == projectInfo.ProjectId);

            Assert.NotNull(project);
        }

        [Test]
        public void AddProjectWithTags_ProjectHasTags()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewProjectForUser(user, InstanceFactory.CreateAddProjectDtoWithTags(user.Id), out ProjectInfoDto projectInfo);
            ProjectInfoDto project = user.ProjectDtos.Find(p => p.ProjectId == projectInfo.ProjectId);

            Assert.True(project.ProjectSkills.Count > 0);
        }
    }
}