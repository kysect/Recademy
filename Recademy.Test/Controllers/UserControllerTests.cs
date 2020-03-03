using NUnit.Framework;
using Recademy.Library.Dto;
using Recademy.Test.Tools;

namespace Recademy.Test.Controllers
{
    public class UserControllerTests
    {
        private readonly TestCaseContext _testContext = new TestCaseContext();

        [Test]
        public void GetUserInfoAfterCreating_NotNull()
        {
            _testContext.WithNewUser(out UserInfoDto _);
        }

        [Test]
        public void FindUserWithWrongId_ReturnNull()
        {
            UserInfoDto user = _testContext.UserController.FindById(1 << 10).Value;

            Assert.Null(user);
        }

        [Test]
        public void FindUserByUsername_Ok()
        {
            _testContext.WithNewUser(out UserInfoDto user);
            UserInfoDto foundedUser = _testContext.UserController.FindByUsername(user.GithubUsername).Value;

            Assert.NotNull(foundedUser);
        }
    }
}