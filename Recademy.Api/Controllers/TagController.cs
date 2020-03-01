using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/tags")]
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
            List<string> allTags = _tagService.GetAllTags();
            return Ok(allTags);
        }

        /// <summary>
        /// Get all existing tags
        /// </summary>
        [HttpGet("{userId}")]
        public ActionResult<List<string>> GetUserTags(int userId)
        {
            List<string> allTags = _tagService.GetUserTags(userId);
            return Ok(allTags);
        }
    }
}