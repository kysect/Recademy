using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Recademy.Api;
using Recademy.Api.Controllers;
using Recademy.Api.Services;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Mock;
using Recademy.Test.Tools;

namespace Recademy.Test.Controllers
{
    public class UserControllerTests
    {
        private RecademyContext _context;
        private Mocker _mocker;
        private UserService _userService;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _context = TestDatabaseProvider.GetDatabaseContext();
            _mocker = new Mocker(_context);
            _userService = new UserService(_context, new AchievementService());
            _userController = new UserController(_userService);
        }

        [Test]
        public void MockDatabase_AtLeastOneUserExist()
        {
            User generatedUser = _mocker.GenerateUser();

            ActionResult<UserInfoDto> result = _userController.GetUserInfo(generatedUser.Id);

            Assert.NotNull(result.Value);
        }
    }
}