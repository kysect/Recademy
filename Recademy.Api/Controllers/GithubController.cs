using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recademy.Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/github")]
    public class GithubController : Controller
    {
        private readonly IGithubService _githubService;

        public GithubController(IGithubService githubService)
        {
            _githubService = githubService;
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{projectUrl}")]
        public IActionResult GetProjectReadme([FromQuery] string projectUrl)
        {
            if (string.IsNullOrWhiteSpace(projectUrl))
                return BadRequest("Wrong project URL");

            MarkupString readme = _githubService.GetReadme(projectUrl);
            return Ok(readme);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("{projectUrl}")]
        public async Task<IActionResult> CreateGithubIssue([FromQuery] string projectUrl, [FromBody] string issueText)
        {
            if (string.IsNullOrWhiteSpace(projectUrl))
                return BadRequest("Wrong project URL");

            if (string.IsNullOrWhiteSpace(issueText))
                return BadRequest("Wrong issue");

            await _githubService.CreateIssues(projectUrl, issueText);

            return Ok();
        }
    }
}
