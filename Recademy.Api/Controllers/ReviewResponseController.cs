﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;

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


        [HttpPost]
        public ActionResult<ReviewRequestInfoDto> CreateReviewResponse([FromBody] [Required] SendReviewResponseDto sendReviewResponseDto)
        {
            return _reviewResponseService.SendReviewResponse(sendReviewResponseDto);
        }
    }
}