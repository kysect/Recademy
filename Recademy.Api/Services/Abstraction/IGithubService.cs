using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Octokit;
using Recademy.Core.Dto;

namespace Recademy.Api.Services.Abstraction
{
    public interface IGithubService
    {
        List<GhRepositoryDto> GhGetRepositories(int userId);
        Issue CreateIssues(GitHubIssueCreateDto issueCreateDto);
        MarkupString GetReadme(string ownerLogin, string repositoryName);
    }
}