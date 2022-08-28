using Microsoft.AspNetCore.Mvc;

using Recademy.Application.Services.Abstractions;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/tags")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<string>> ReadAll()
        {
            return Ok(_tagService.GetAllTags());
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IReadOnlyCollection<string>> ReadByUserId([Required] int userId)
        {
            return Ok(_tagService.GetUserTags(userId));
        }
    }
}