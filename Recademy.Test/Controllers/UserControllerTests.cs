using NUnit.Framework;
using Recademy.Library.Dto;
using Recademy.Test.Tools;

namespace Recademy.Test.Controllers
{
    public class UserControllerTests
    {
        private readonly TestCaseContext _testContext = new TestCaseContext();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetUserInfoAfterCreating_NotNull()
        {
            _testContext.WithNewUser(out UserInfoDto _);
        }
    }
}