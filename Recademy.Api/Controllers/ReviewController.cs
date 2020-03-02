using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Types;

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
            return reviewRequestAddDto switch
            {
                null => BadRequest(RecademyException.MissedArgument(nameof(reviewRequestAddDto))),
                _ => _reviewService.AddReviewRequest(reviewRequestAddDto)
            };
        }

        /// <summary>
        /// Get review request info
        /// </summary>
        [HttpGet("{requestId}")]
        public ActionResult<ReviewRequestInfoDto> GetReviewRequestInfo(int? requestId)
        {
            return requestId switch
            {
                null => BadRequest(RecademyException.MissedArgument(nameof(requestId))),
                _ when requestId < 0 => BadRequest(RecademyException.InvalidArgument(nameof(requestId), requestId)),
                _ => Ok(_reviewService.GetReviewInfo(requestId.Value))
            };
        }

        /// <summary>
        /// Create review response info
        /// </summary>
        [HttpPost("{requestId}/review")]
        public ActionResult<ReviewRequestInfoDto> CreateReviewResponse(int? requestId, [FromBody] SendReviewResponseDto sendReviewResponseDto)
        {
            return requestId switch
            {
                null => BadRequest(RecademyException.MissedArgument(nameof(requestId))),
                _ when requestId < 0 => BadRequest(RecademyException.InvalidArgument(nameof(requestId), requestId)),
                _ when sendReviewResponseDto == null => BadRequest(RecademyException.MissedArgument(nameof(sendReviewResponseDto))),
                _ => Ok(_reviewService.SendReviewResponse(requestId.Value, sendReviewResponseDto))
            };
        }

        /// <summary>
        /// Get review requests list
        /// </summary>
        [HttpGet]
        public ActionResult<List<ReviewRequestInfoDto>> GetReviewRequestInfo()
        {
            return Ok(_reviewService.GetReviewRequests());
        }
    }
}