using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Types;

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

        /// <summary>
        /// Get all existing tags
        /// </summary>
        [HttpGet]
        public ActionResult<List<string>> GetAllExistingTags()
        {
            return _tagService.GetAllTags();
        }

        /// <summary>
        /// Get all existing tags
        /// </summary>
        [HttpGet("{userId}")]
        public ActionResult<List<string>> GetUserTags([Required]int userId)
        {
            return userId switch
            {
                _ when userId < 0 => BadRequest(RecademyException.InvalidArgument(nameof(userId), userId)),
                _ => Ok(_tagService.GetUserTags(userId))
            };
        }
    }
}