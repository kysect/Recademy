using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recademy.Api.Controllers
{
    [Route("api/review")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [Route("{projectId}")]
        public IActionResult CreateReviewRequest([FromQuery] int projectId)
        {
            if (projectId < 0)
                return BadRequest("Wrong project id");

            _reviewService.AddReviewRequest(projectId);
            return Accepted();
        }

        [HttpGet]
        [Route("{requestId}")]
        public IActionResult GetReviewRequestInfo([FromQuery] int requestId)
        {
            if (requestId < 0)
                return BadRequest("Wrong request id");

            ReviewProjectDto reviewInfo = _reviewService.GetReviewInfo(requestId);
            return Ok(reviewInfo);
        }
        [HttpPost]
        [Route("{requestId}")]
        public IActionResult CreateReviewResponse([FromQuery] int requestId, [FromBody] string reviewText)
        {
            if (requestId < 0)
                return BadRequest("Wrong request id");

            if (string.IsNullOrWhiteSpace(reviewText))
                return BadRequest("Wrong issue");

            _reviewService.SendReviewResponse(new SendReviewRequestDto(requestId, reviewText));
            return Ok();
        }
    }
}