using Microsoft.AspNetCore.Mvc;

using Recademy.Application.Services.Abstractions;
using Recademy.Dto.Reviews;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [HttpPost]
        public ActionResult<ReviewRequestInfoDto> CreateReviewRequest([FromBody][Required] ReviewRequestAddDto reviewRequestAddDto)
        {
            return _reviewService.AddReviewRequest(reviewRequestAddDto);
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<ReviewRequestInfoDto>> ReadAllRequest()
        {
            return Ok(_reviewService.GetReviewRequests());
        }

        [HttpGet("{requestId}")]
        public ActionResult<ReviewRequestInfoDto> ReadRequestById([Required] int requestId)
        {
            return _reviewService.GetReviewInfo(requestId);
        }

        [HttpPost("search")]
        public ActionResult<IReadOnlyCollection<ReviewRequestInfoDto>> ReadReviewRequestBySearchContext(ReviewRequestSearchContextDto searchContextDto)
        {
            return Ok(_reviewService.ReadReviewRequestBySearchContext(searchContextDto));
        }

        [HttpPost("{requestId}/complete")]
        public ActionResult<ReviewRequestInfoDto> CompleteReview([Required] int requestId)
        {
            return _reviewService.CompleteReview(requestId);
        }

        [HttpPost("{requestId}/abandon")]
        public ActionResult<ReviewRequestInfoDto> AbandonReview([Required] int requestId)
        {
            return _reviewService.AbandonReview(requestId);
        }
    }
}