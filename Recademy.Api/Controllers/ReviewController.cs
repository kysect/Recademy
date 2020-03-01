using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;

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
        public ActionResult<ReviewRequestInfoDto> CreateReviewRequest([FromBody] ReviewRequestAddDto reviewRequestAddDto)
        {
            ReviewRequestInfoDto result = _reviewService.AddReviewRequest(reviewRequestAddDto);
            return Accepted(result);
        }

        /// <summary>
        /// Get review request info
        /// </summary>
        [HttpGet]
        [Route("{requestId}")]
        public ActionResult<ReviewRequestInfoDto> GetReviewRequestInfo(int requestId)
        {
            if (requestId < 0)
                return BadRequest("Wrong request id");

            ReviewRequestInfoDto reviewInfo = _reviewService.GetReviewInfo(requestId);
            return Ok(reviewInfo);
        }

        /// <summary>
        /// Create review response info
        /// </summary>
        [HttpPost]
        [Route("{requestId}")]
        public ActionResult<ReviewRequestInfoDto> CreateReviewResponse(int requestId, [FromBody] string reviewText)
        {
            if (requestId < 0)
                return BadRequest("Wrong request id");

            if (string.IsNullOrWhiteSpace(reviewText))
                return BadRequest("Wrong issue");

            ReviewRequestInfoDto result = _reviewService.SendReviewResponse(new SendReviewResponseDto(requestId, reviewText));
            return Ok(result);
        }

        /// <summary>
        /// Get review requests list
        /// </summary>
        [HttpGet]
        public ActionResult<List<ReviewRequestInfoDto>> GetReviewRequestInfo()
        {
            List<ReviewRequestInfoDto> reviewRequests = _reviewService.GetReviewRequests();
            return Ok(reviewRequests);
        }
    }
}