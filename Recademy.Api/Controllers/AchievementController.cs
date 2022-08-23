using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recademy.Api.Services.Abstraction;
using Recademy.Core.Dto;

namespace Recademy.Api.Controllers
{
    [Route("api/achievements")]
    [ApiController]
    public class AchievementController : Controller
    {
        private readonly IUserAchievementService _userAchievementService;
        private readonly ILogger<AchievementController> _logger;

        public AchievementController(IUserAchievementService userAchievementService, ILogger<AchievementController> logger)
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
    }
}