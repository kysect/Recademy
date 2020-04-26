using NUnit.Framework;
using Recademy.Library.Dto;
using Recademy.Library.Types;
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
        public void FindUserByUsername_Ok()
        {
            _testContext.WithNewUser(out UserInfoDto user);
            UserInfoDto foundedUser = _testContext.UserController.ReadByUsername(user.GithubUsername).Value;

            Assert.NotNull(foundedUser);
        }

        [Test]
        public void SetMentorType_NewTypeIsMentor()
        {
            _testContext
                .WithNewUser(out UserInfoDto user)
                .WithNewAdminUser(out UserInfoDto admin);

            UserInfoDto foundedUser = _testContext.UserController.UpdateSetMentorRole(admin.Id, user.Id).Value;

            Assert.AreEqual(UserType.Mentor, foundedUser.UserType);
        }

        [Test]
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