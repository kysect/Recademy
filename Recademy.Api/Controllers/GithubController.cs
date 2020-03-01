using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/github")]
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
        [HttpGet("readme")]
        public ActionResult<Microsoft.AspNetCore.Components.MarkupString> GetProjectReadme([FromQuery] string projectUrl)
        {
            if (string.IsNullOrWhiteSpace(projectUrl))
                return BadRequest("Wrong project URL");

            Microsoft.AspNetCore.Components.MarkupString readme = _githubService.GetReadme(projectUrl);
            return Ok(readme);
        }

        /// <summary>
        /// Create issue to project on github
        /// </summary>
        [HttpPost("issue")]
        public async Task<ActionResult<Issue>> CreateGithubIssue([FromQuery] string projectUrl, [FromBody] string issueText)
        {
            if (string.IsNullOrWhiteSpace(projectUrl))
                return BadRequest("Wrong project URL");

            if (string.IsNullOrWhiteSpace(issueText))
                return BadRequest("Wrong issue");

            Issue issue = await _githubService.CreateIssues(projectUrl, issueText);
            return Ok(issue);
        }

        /// <summary>
        /// Get user projects from github
        /// </summary>
        [HttpGet("projects/{userId}")]
        public ActionResult<List<GhRepositoryDto>> GetUserRepositories(int userId)
        {
            userId = 1; // Пока нет авторизации
            if (userId < 0)
                return BadRequest("Wrong user id");

            List<GhRepositoryDto> repositories = _githubService.GhGetRepositories(userId);
            return Ok(repositories);
        }
    }
}
