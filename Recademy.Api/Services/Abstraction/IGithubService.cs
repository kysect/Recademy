using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Octokit;
using Recademy.Library.Dto;

namespace Recademy.Api.Services.Abstraction
{
    public interface IGithubService
    {
        List<GhRepositoryDto> GhGetRepositories(int userId);
        Issue CreateIssues(string projectUrl, string issueText);
        MarkupString GetReadme(string projectUrl);
    }
}