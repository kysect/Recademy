using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Shared.Dtos;

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/review")]
    [ApiController]
    public class ReviewResponseController
    {
        private readonly IReviewResponseService _reviewResponseService;

        public ReviewResponseController(IReviewResponseService reviewResponseService)
        {
            _reviewResponseService = reviewResponseService;
        }


        [HttpPost("create")]
        public ActionResult<ReviewResponseInfoDto> CreateReviewResponse([FromBody] [Required] ReviewResponseCreateDto reviewResponseCreateDto)
        {
            return _reviewResponseService.SendReviewResponse(reviewResponseCreateDto);
        }
    }
}