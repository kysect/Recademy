using Microsoft.AspNetCore.Mvc;
using Recademy.Application.Services.Abstractions;
using Recademy.Dto.Reviews;
using System.ComponentModel.DataAnnotations;

namespace Recademy.Api.Controllers;

[Produces("application/json")]
[Consumes("application/json")]
[Route("api/reviews/responses")]
[ApiController]
public class ReviewResponseController
{
    private readonly IReviewResponseService _reviewResponseService;

    public ReviewResponseController(IReviewResponseService reviewResponseService)
    {
        _reviewResponseService = reviewResponseService;
    }


    [HttpPost]
    public ActionResult<ReviewResponseInfoDto> CreateReviewResponse([FromBody][Required] ReviewResponseCreateDto reviewResponseCreate)
    {
        return _reviewResponseService.SendReviewResponse(reviewResponseCreate);
    }
}