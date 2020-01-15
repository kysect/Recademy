using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recademy.Api.Controllers
{
    [Route("api/tags")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [Route("{tagName}")]
        public IActionResult GetTagProjects([FromQuery] string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                return BadRequest("Wrong tag name");

            TagProfileDto tagProfile = _tagService.GetTagProfile(tagName);

            return Ok(tagProfile);
        }

    }
}