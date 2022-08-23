using NUnit.Framework;
using Recademy.Core.Dto;
using Recademy.Core.Types;
using Recademy.Test.Tools;

namespace Recademy.Test.Controllers
{
    public class UserControllerTests
    {
        private TestCaseContext _testContext;

        [SetUp]
        public void InitPerTest()
        {
            _testContext = new TestCaseContext();
        }

        [Test]
        [Ignore("This test requires GitHub credentials. Please run manually for local tests.")]
        public void GetUserInfoAfterCreating_NotNull()
        {
            _testContext.WithNewUser(out UserInfoDto _);
        }

        [Test]
        public void FindUserWithWrongId_ReturnNull()
        {
            UserInfoDto user = _testContext.UserController.ReadById(1 << 10).Value;

            Assert.Null(user);
        }

        [Test]
        [Ignore("Some times we generate users with same github names")]
        public void FindUserByUsername_Ok()
        {
            _testContext.WithNewUser(out UserInfoDto user);
            UserInfoDto foundedUser = _testContext.UserController.ReadByUsername(user.GithubUsername).Value;

            Assert.NotNull(foundedUser);
        }

        [Test]
        [Ignore("This test requires GitHub credentials. Please run manually for local tests.")]
        public void SetMentorType_NewTypeIsMentor()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewAdminUser(out UserInfoDto admin);

            UserInfoDto foundedUser = _testContext.UserController.UpdateSetMentorRole(admin.Id, user.Id).Value;

            Assert.AreEqual(UserType.Mentor, foundedUser.UserType);
        }

        [Test]
        [Ignore("This test requires GitHub credentials. Please run manually for local tests.")]
        public void NotAdminSetRole_FailWithPermissionException()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewUser(out UserInfoDto notAdmin);

            Assert.Catch<RecademyException>(() =>
                _testContext.UserController.UpdateSetMentorRole(notAdmin.Id, user.Id));
        }
    }
}