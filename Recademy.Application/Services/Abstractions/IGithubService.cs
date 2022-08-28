using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Octokit;
using Recademy.Shared.Dtos.Github;

namespace Recademy.Application.Services.Abstractions
{
    public interface IGithubService
    {
        List<GithubRepositoryDto> GhGetRepositories(int userId);
        Issue CreateIssues(GitHubIssueCreateDto issueCreateDto);
        MarkupString GetReadme(string ownerLogin, string repositoryName);
    }
}