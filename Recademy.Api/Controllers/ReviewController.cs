using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;
using Recademy.Dto.Reviews;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recademy.Api.Controllers;

[Produces("application/json")]
[Consumes("application/json")]
[Route("api/reviews")]
[ApiController]
public class ReviewController : Controller
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost("requests")]
    public ActionResult<ReviewRequestInfoDto> CreateReviewRequest([FromBody][Required] ReviewRequestAddDto reviewRequestAddDto)
    {
        return _reviewService.AddReviewRequest(reviewRequestAddDto);
    }

    [HttpGet("requests")]
    public ActionResult<IReadOnlyCollection<ReviewRequestInfoDto>> GetAllRequest()
    {
        return Ok(_reviewService.GetReviewRequests());
    }

    [HttpGet("requests/{requestId}")]
    public ActionResult<ReviewRequestInfoDto> GetRequestById([Required] int requestId)
    {
        return _reviewService.GetReviewInfo(requestId);
    }

    [HttpGet("requests/search")]
    public ActionResult<IReadOnlyCollection<ReviewRequestInfoDto>> GetReviewRequestBySearchContext([FromBody] ReviewRequestSearchContextDto searchContext)
    {
        return Ok(_reviewService.ReadReviewRequestBySearchContext(searchContext));
    }

    [HttpPost("requests/{requestId}/complete")]
    public ActionResult<ReviewRequestInfoDto> CompleteReview([Required] int requestId)
    {
        return _reviewService.CompleteReview(requestId);
    }

    [HttpPost("requests/{requestId}/abandon")]
    public ActionResult<ReviewRequestInfoDto> AbandonReview([Required] int requestId)
    {
        return _reviewService.AbandonReview(requestId);
    }
}