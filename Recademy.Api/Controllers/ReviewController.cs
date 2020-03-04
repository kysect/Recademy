using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Types;

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/review")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        ///     Create review request
        /// </summary>
        [HttpPost]
        public ActionResult<ReviewRequestInfoDto> CreateReviewRequest(
            [FromBody] [Required] ReviewRequestAddDto reviewRequestAddDto)
        {
            return _reviewService.AddReviewRequest(reviewRequestAddDto);
        }

        /// <summary>
        ///     Get review request info
        /// </summary>
        [HttpGet("{requestId}")]
        public ActionResult<ReviewRequestInfoDto> GetReviewRequestInfo([Required] int requestId)
        {
            return requestId switch
            {
                _ when requestId < 0 => BadRequest(RecademyException.InvalidArgument(nameof(requestId), requestId)),
                _ => Ok(_reviewService.GetReviewInfo(requestId))
            };
        }

        [HttpPost("search")]
        public ActionResult<List<ReviewRequestInfoDto>> ReadReviewRequestBySearchContext(ReviewRequestSearchContextDto searchContextDto)
        {
            return _reviewService.ReadReviewRequestBySearchContext(searchContextDto);
        }

        /// <summary>
        ///     Create review response info
        /// </summary>
        [HttpPost("{requestId}/review")]
        public ActionResult<ReviewRequestInfoDto> CreateReviewResponse([Required] int requestId,
            [FromBody] [Required] SendReviewResponseDto sendReviewResponseDto)
        {
            return requestId switch
            {
                _ when requestId < 0 => BadRequest(RecademyException.InvalidArgument(nameof(requestId), requestId)),
                _ => Ok(_reviewService.SendReviewResponse(requestId, sendReviewResponseDto))
            };
        }

        [HttpPost("{requestId}/complete")]
        public ActionResult<ReviewRequestInfoDto> CompleteReview([Required] int requestId)
        {
            return requestId switch
            {
                _ when requestId < 0 => BadRequest(RecademyException.InvalidArgument(nameof(requestId), requestId)),
                _ => _reviewService.CompleteReview(requestId)
            };
        }

        [HttpPost("{requestId}/abandon")]
        public ActionResult<ReviewRequestInfoDto> AbandonReview([Required] int requestId)
        {
            return requestId switch
            {
                _ when requestId < 0 => BadRequest(RecademyException.InvalidArgument(nameof(requestId), requestId)),
                _ => _reviewService.AbandonReview(requestId)
            };
        }

        /// <summary>
        ///     Get review requests list
        /// </summary>
        [HttpGet]
        public ActionResult<List<ReviewRequestInfoDto>> GetReviewRequestInfo()
        {
            return _reviewService.GetReviewRequests();
        }
    }
}