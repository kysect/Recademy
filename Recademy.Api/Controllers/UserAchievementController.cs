using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recademy.Api.Services.Abstraction;
using Recademy.Core.Dto;

namespace Recademy.Api.Controllers
{
    [Route("api/achievements")]
    [ApiController]
    public class UserAchievementController : Controller
    {
        private readonly IUserAchievementService _userAchievementService;
        private readonly ILogger<UserAchievementController> _logger;

        public UserAchievementController(IUserAchievementService userAchievementService, ILogger<UserAchievementController> logger)
        {
            _userAchievementService = userAchievementService;
            _logger = logger;
        }

        [HttpGet("users/list")]
        public ActionResult<IReadOnlyCollection<UserAchievementDto>> GetAllAchievements()
        {
            IReadOnlyCollection<UserAchievementDto> achievements = _userAchievementService
                .GetAllAchievements()
                .Select(achievement => new UserAchievementDto(achievement.Id, achievement.Title, achievement.Description, achievement.Points))
                .ToList();

            return Ok(achievements);
        }

        [HttpGet("users/{userId}")]
        public ActionResult<IReadOnlyCollection<UserAchievementDto>> GetUserAchievements(int userId)
        {
            IReadOnlyCollection<UserAchievementDto> achievements = _userAchievementService.GetUserAchievements(userId)
                .Select(achievement => new UserAchievementDto(achievement.Id, achievement.Title, achievement.Description, achievement.Points))
                .ToList();

            return Ok(achievements);
        }

        [HttpGet("users/{userId}/points")]
        public ActionResult<int> GetUserAchievementPoints(int userId)
        {
            Int32 userAchievementPoints = _userAchievementService.GetUserAchievementPoints(userId);

            return Ok(userAchievementPoints);
        }

        [HttpPost("users/{userId}/{achievementId}")]
        public async Task<IActionResult> AddUserAchievement(int userId, int achievementId)
        {
            await _userAchievementService.AddUserAchievement(userId, achievementId);
            return Ok();
        }
    }
}