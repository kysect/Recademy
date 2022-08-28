using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;

namespace Recademy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamificationController : ControllerBase
    {
        private readonly IGamificationService _gamificationService;

        public GamificationController(IGamificationService gamificationService)
        {
            _gamificationService = gamificationService;
        }

        [HttpPost("review/{reviewId}/upvote")]
        public IActionResult CreateReviewUpvote([FromQuery] [Required] int reviewId, [FromQuery] [Required] int userId)
        {
            _gamificationService.CreateReviewResponseUpvote(reviewId, userId);
            return Ok();
        }

        [HttpGet("review/{reviewId}/upvote")]
        public ActionResult<List<int>> ReadReviewUpvote([FromQuery] [Required] int reviewId)
        {
            return _gamificationService.ReadReviewResponseUpvote(reviewId);
        }


        [HttpDelete("review/{reviewId}/upvote")]
        public IActionResult DeleteReviewUpvote([FromQuery] [Required] int reviewId, [FromQuery] [Required] int userId)
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
        public ActionResult<int> ReadUserKarmaPointCount([Required] int userId)
        {
            return _gamificationService.ReadUserKarmaPointCount(userId);
        }
    }
}