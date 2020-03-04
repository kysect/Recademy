using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Types;

namespace Recademy.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Microsoft.AspNetCore.Mvc.Route("api/github")]
    [ApiController]
    public class GithubController : Controller
    {
        private readonly IGithubService _githubService;

        public GithubController(IGithubService githubService)
        {
            _githubService = githubService;
        }

        /// <summary>
        ///     Get project readme
        /// </summary>
        [HttpGet("readme")]
        public ActionResult<MarkupString> GetProjectReadme([FromQuery] [Required] string projectUrl)
        {
            //TODO: don't send url via query, send as json in post
            MarkupString readme = _githubService.GetReadme(projectUrl);
            return Ok(readme);
        }

        /// <summary>
        ///     Create issue to project on github
        /// </summary>
        [HttpPost("issue")]
        public ActionResult<Issue> CreateGithubIssue([FromBody] GitHubIssueCreateDto createDto)
        {
            return _githubService.CreateIssues(createDto.ProjectUrl, createDto.IssueText);
        }

        /// <summary>
        ///     Get user projects from github
        /// </summary>
        [HttpGet("projects/{userId}")]
        public ActionResult<List<GhRepositoryDto>> GetUserRepositories([Required] int userId)
        {
            return userId switch
            {
                _ when userId < 0 => BadRequest(RecademyException.InvalidArgument(nameof(userId), userId)),
                _ => Ok(_githubService.GhGetRepositories(userId))
            };
        }
    }
}