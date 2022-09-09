using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Recademy.Application.Services.Abstractions;
using Recademy.Core.Types;
using Recademy.Dto.Github;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [HttpPost("{ownerLogin}/{repositoryName}/issues")]
        public ActionResult<Issue> CreateGithubIssue([FromBody] GitHubIssueCreateDto issueCreateDto)
        {
            return _githubService.CreateIssues(issueCreateDto);
        }

        [HttpGet("{ownerLogin}/{repositoryName}/readme")]
        public ActionResult<MarkupString> GetProjectReadme([FromQuery][Required] string ownerLogin, [FromQuery][Required] string repositoryName)
        {
            MarkupString readme = _githubService.GetReadme(ownerLogin, repositoryName);
            return Ok(readme);
        }

        [HttpGet("projects/{userId}")]
        public ActionResult<List<GithubRepositoryDto>> GetRepositoriesByUserId([Required] int userId)
        {
            return userId switch
            {
                _ when userId < 0 => BadRequest(RecademyException.InvalidArgument(nameof(userId), userId)),
                _ => Ok(_githubService.GhGetRepositories(userId))
            };
        }
    }
}