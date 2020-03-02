using NUnit.Framework;
using Recademy.Api;
using Recademy.Api.Services;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Mock;
using Recademy.Test.Tools;

namespace Recademy.Test.Services
{
    public class UserServiceTests
    {
        private RecademyContext _context;
        private Mocker _mocker;
        private UserService _userService; 

        [SetUp]
        public void Setup()
        {
            _context = TestDatabaseProvider.GetDatabaseContext();
            _mocker = new Mocker(_context);
            _userService = new UserService(_context, new AchievementService());
        }

        [Test]
        public void MockDatabase_AtLeastOneUserExist()
        {
            User generatedUser = _mocker.GenerateUser();

            UserInfoDto userFromDb = _userService.GetUserInfo(generatedUser.Id);

            Assert.NotNull(userFromDb);
        }
    }
}