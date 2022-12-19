using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Attributes;
using Recademy.Application.Services.Abstractions;
using Recademy.Dto.Reviews;
using Recademy.Dto.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recademy.Api.Controllers;

[Route("api/reviews")]
[ApiController]
public class ReviewsController : Controller
{
    private readonly IReviewService _reviewService;
    private readonly IAuthService _authService;

    public ReviewsController(IReviewService reviewService, IAuthService authService)
    {
        _reviewService = reviewService;
        _authService = authService;
    }

    [RoleRequired]
    [HttpGet("requests")]
    public async Task<ActionResult<IReadOnlyCollection<ReviewRequestInfoDto>>> GetReviewRequests()
    {
        IReadOnlyCollection<ReviewRequestInfoDto> reviewRequests = await _reviewService.GetReviewRequests();
        return Ok(reviewRequests);
    }

    [HttpGet("requests/user")]
    public async Task<ActionResult<IReadOnlyCollection<ReviewRequestInfoDto>>> GetCurrentUserReviewRequests()
    {
        UserInfoDto dto = _authService.GetCurrentUser(HttpContext.User);
        IReadOnlyCollection<ReviewRequestInfoDto> reviewRequests = await _reviewService.GetReviewRequestsByUserId(dto.Id);

        return Ok(reviewRequests);
    }

    [HttpGet("requests/{requestId}")]
    public ActionResult<ReviewRequestInfoDto> GetReviewRequestById(int requestId)
    {
        ReviewRequestInfoDto reviewRequest = _reviewService.GetReviewRequestById(requestId);
        return Ok(reviewRequest);
    }

    [HttpGet("responses/{projectId}")]
    public ActionResult<IReadOnlyCollection<ReviewResponseInfoDto>> GetReviewResponsesByProjectId(int projectId)
    {
        IReadOnlyCollection<ReviewResponseInfoDto> reviewResponses = _reviewService.GetReviewResponsesByProjectId(projectId);
        return Ok(reviewResponses);
    }

    [HttpPost("requests")]
    public async Task<ActionResult<ReviewRequestInfoDto>> CreateReviewRequest(CreateReviewRequestDto createReviewRequestDto)
    {
        ReviewRequestInfoDto reviewRequest = await _reviewService.CreateReviewRequest(createReviewRequestDto);
        return Ok(reviewRequest);
    }

    [HttpPost("responses")]
    public async Task<ActionResult<ReviewResponseInfoDto>> CreateReviewResponse(CreateReviewResponseDto createReviewResponseDto)
    {
        ReviewResponseInfoDto reviewResponse = await _reviewService.CreateReviewResponse(createReviewResponseDto);
        return Ok(reviewResponse);
    }
}
