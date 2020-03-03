using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;

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
        public IActionResult ReadReviewUpvote([FromQuery] [Required] int reviewId)
        {
            _gamificationService.ReadReviewResponseUpvote(reviewId);
            return Ok();
        }


        [HttpDelete("review/{reviewId}/upvote")]
        public IActionResult DeleteReviewUpvote([FromQuery] [Required] int reviewId, [FromQuery] [Required] int userId)
        {
            _gamificationService.DeleteReviewResponseUpvote(reviewId, userId);
            return Ok();
        }
    }
}