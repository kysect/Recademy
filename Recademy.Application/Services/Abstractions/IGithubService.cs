using Microsoft.AspNetCore.Components;

using Octokit;

using Recademy.Dto.Github;

using System.Collections.Generic;

namespace Recademy.Application.Services.Abstractions
{
    public interface IGithubService
    {
        List<GithubRepositoryDto> GhGetRepositories(int userId);
        Issue CreateIssues(GitHubIssueCreateDto issueCreateDto);
        MarkupString GetReadme(string ownerLogin, string repositoryName);
    }
}