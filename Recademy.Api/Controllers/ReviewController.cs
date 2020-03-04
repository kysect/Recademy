using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;

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
        public ActionResult<ReviewRequestInfoDto> CreateReviewRequest([FromBody] [Required] ReviewRequestAddDto reviewRequestAddDto)
        {
            return _reviewService.AddReviewRequest(reviewRequestAddDto);
        }

        [HttpGet]
        public ActionResult<List<ReviewRequestInfoDto>> ReadAllRequest()
        {
            return _reviewService.GetReviewRequests();
        }

        [HttpGet("{requestId}")]
        public ActionResult<ReviewRequestInfoDto> ReadRequestById([Required] int requestId)
        {
            return _reviewService.GetReviewInfo(requestId);
        }

        [HttpPost("search")]
        public ActionResult<List<ReviewRequestInfoDto>> ReadReviewRequestBySearchContext(ReviewRequestSearchContextDto searchContextDto)
        {
            return _reviewService.ReadReviewRequestBySearchContext(searchContextDto);
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