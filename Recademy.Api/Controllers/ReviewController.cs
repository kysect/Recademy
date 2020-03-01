using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/review")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Create review request
        /// </summary>
        [HttpPost]
        [Route("{projectId}")]
        public IActionResult CreateReviewRequest(int projectId)
        {
            if (projectId < 0)
                return BadRequest("Wrong project id");

            _reviewService.AddReviewRequest(projectId);
            return Accepted();
        }

        /// <summary>
        /// Get review request info
        /// </summary>
        [HttpGet]
        [Route("{requestId}")]
        public IActionResult GetReviewRequestInfo(int requestId)
        {
            if (requestId < 0)
                return BadRequest("Wrong request id");

            ReviewProjectDto reviewInfo = _reviewService.GetReviewInfo(requestId);
            return Ok(reviewInfo);
        }

        /// <summary>
        /// Create review response info
        /// </summary>
        [HttpPost]
        [Route("{requestId}")]
        public IActionResult CreateReviewResponse(int requestId, [FromBody] string reviewText)
        {
            if (requestId < 0)
                return BadRequest("Wrong request id");

            if (string.IsNullOrWhiteSpace(reviewText))
                return BadRequest("Wrong issue");

            _reviewService.SendReviewResponse(new SendReviewRequestDto(requestId, reviewText));
            return Ok();
        }

        /// <summary>
        /// Get review requests list
        /// </summary>
        [HttpGet]
        public IActionResult GetReviewRequestInfo()
        {
            List<ReviewRequest> reviewRequests = _reviewService.GetReviewRequests();
            return Ok(reviewRequests);
        }
    }
}