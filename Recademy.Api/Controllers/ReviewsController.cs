using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;
using Recademy.Dto.Reviews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recademy.Api.Controllers;

[Route("api/reviews")]
[ApiController]
public class ReviewsController : Controller
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("requests")]
    public async Task<ActionResult<IReadOnlyCollection<ReviewRequestInfoDto>>> GetReviewRequests()
    {
        IReadOnlyCollection<ReviewRequestInfoDto> reviewRequests = await _reviewService.GetReviewRequests();
        return Ok(reviewRequests);
    }

    [HttpGet("requests/{requestId}")]
    public ActionResult<ReviewRequestInfoDto> GetReviewRequestById(int requestId)
    {
        ReviewRequestInfoDto reviewRequest = _reviewService.GetReviewRequestById(requestId);
        return Ok(reviewRequest);
    }

    [HttpPost("requests")]
    public ActionResult<ReviewRequestInfoDto> CreateReviewRequest(CreateReviewRequestDto createReviewRequestDto)
    {
        ReviewRequestInfoDto reviewRequest = _reviewService.CreateReviewRequest(createReviewRequestDto);
        return Ok(reviewRequest);
    }
}
