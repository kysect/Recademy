using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recademy.Api.Controllers
{
    [Route("api/gamification")]
    [ApiController]
    public class GamificationController : ControllerBase
    {
        private readonly IGamificationService _gamificationService;

        public GamificationController(IGamificationService gamificationService)
        {
            _gamificationService = gamificationService;
        }

        [HttpPost("users/{userId}/reviews/{reviewId}/upvote")]
        public IActionResult CreateReviewUpvote([Required] int reviewId, [Required] int userId)
        {
            _gamificationService.CreateReviewResponseUpvote(reviewId, userId);
            return Ok();
        }

        [HttpGet("reviews/{reviewId}/upvote")]
        public ActionResult<IReadOnlyCollection<int>> GetReviewUpvote([Required] int reviewId)
        {
            return Ok(_gamificationService.ReadReviewResponseUpvote(reviewId));
        }


        [HttpDelete("users/{userId}/reviews/{reviewId}/upvote")]
        public IActionResult DeleteReviewUpvote([Required] int reviewId, [Required] int userId)
        {
            _gamificationService.DeleteReviewResponseUpvote(reviewId, userId);
            return Ok();
        }

        [HttpGet("ranking")]
        public ActionResult<Dictionary<string, int>> GetUsersRanking()
        {
            return _gamificationService.GetUsersRanking();
        }

        [HttpGet("karma")]
        public ActionResult<int> GetUserKarmaPointCount([Required] int userId)
        {
            return _gamificationService.GetUserKarmaPointCount(userId);
        }
    }
}