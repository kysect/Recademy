using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Microsoft.AspNetCore.Mvc.Route("api/github")]
    public class GithubController : Controller
    {
        private readonly IGithubService _githubService;

        public GithubController(IGithubService githubService)
        {
            _githubService = githubService;
        }

        /// <summary>
        /// Get project readme
        /// </summary>
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("readme")]
        public IActionResult GetProjectReadme([FromQuery] string projectUrl)
        {
            if (string.IsNullOrWhiteSpace(projectUrl))
                return BadRequest("Wrong project URL");

            MarkupString readme = _githubService.GetReadme(projectUrl);
            return Ok(readme);
        }

        /// <summary>
        /// Create issue to project on github
        /// </summary>
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("issue")]    
        public async Task<IActionResult> CreateGithubIssue([FromQuery] string projectUrl, [FromBody] string issueText)
        {
            if (string.IsNullOrWhiteSpace(projectUrl))
                return BadRequest("Wrong project URL");

            if (string.IsNullOrWhiteSpace(issueText))
                return BadRequest("Wrong issue");

            await _githubService.CreateIssues(projectUrl, issueText);

            return Ok();
        }

        /// <summary>
        /// Get user projects from github
        /// </summary>
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("projects/{userId}")]
        public IActionResult GetUserRepositories(int userId)
        {
            userId = 1; // Пока нет авторизации
            if (userId < 0)
                return BadRequest("Wrong user id");

            List<GhRepositoryDto> repositories = _githubService.GhGetRepositories(userId);
            return Ok(repositories);
        }
    }
}
