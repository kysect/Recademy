using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Types;

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
            //TODO: don't send url via query, send as json in post
            if (string.IsNullOrWhiteSpace(projectUrl))
                return BadRequest("Wrong project URL");

            Microsoft.AspNetCore.Components.MarkupString readme = _githubService.GetReadme(projectUrl);
            return Ok(readme);
        }

        /// <summary>
        /// Create issue to project on github
        /// </summary>
        [HttpPost("issue")]
        public async Task<ActionResult<Issue>> CreateGithubIssue([FromBody] GitHubIssueCreateDto createDto)
        {
            return createDto switch
            {
                null => BadRequest(RecademyException.MissedArgument(nameof(createDto))),

                {ProjectUrl: null} => BadRequest(
                    RecademyException.MissedArgument(nameof(GitHubIssueCreateDto.ProjectUrl))),

                {IssueText: null} => BadRequest(
                    RecademyException.MissedArgument(nameof(GitHubIssueCreateDto.IssueText))),

                _ => Ok(await _githubService.CreateIssues(createDto.ProjectUrl, createDto.IssueText))
            };
        }

        /// <summary>
        /// Get user projects from github
        /// </summary>
        [HttpGet("projects/{userId}")]
        public ActionResult<List<GhRepositoryDto>> GetUserRepositories(int? userId)
        {
            return userId switch
            {
                null => BadRequest(RecademyException.MissedArgument(nameof(userId))),
                _ when userId < 0 => BadRequest(RecademyException.InvalidArgument(nameof(userId), userId)),
                _ => Ok(_githubService.GhGetRepositories(userId.Value))
            };
        }
    }
}
