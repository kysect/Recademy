using Microsoft.AspNetCore.Components;

using Octokit;

using Recademy.Application.Services.Abstractions;
using Recademy.Application.Tools;
using Recademy.Dto;
using Recademy.Dto.Github;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Recademy.Application.Services.Implementations;

public class GithubService : IGithubService
{
    private readonly IGithubApiAccessor _githubApiAccessor;

    public GithubService(IGithubApiAccessor githubApiAccessor)
    {
        _githubApiAccessor = githubApiAccessor;
    }

    public IReadOnlyCollection<GithubRepositoryDto> GhGetRepositories(int userId)
    {
        //TODO: get token by userId
        IReadOnlyList<Repository> repositories = _githubApiAccessor.ReadAllUserRepositories(String.Empty);

        return repositories
            .Where(repository => !repository.Private)
            .Select(repository => new GithubRepositoryDto
            {
                RepositoryName = repository.Name,
                RepositoryUrl = repository.Url,
                Readme = _githubApiAccessor.GetReadme(repository.Owner.Login, repository.Name),
                Language = repository.Language
            })
            .ToList();
    }

    public Issue CreateIssues(GitHubIssueCreateDto issueCreateDto)
    {
        string issueTitle = $"{GhUtil.IssueText} {issueCreateDto.IssueTitle}";
        var issue = new NewIssue(issueTitle)
        {
            Body = issueCreateDto.IssueText
        };

        return _githubApiAccessor.CreateIssue(issueCreateDto.OwnerLogin, issueCreateDto.RepositoryName, issue);
    }

    public MarkupString GetReadme(string ownerLogin, string repositoryName)
    {
        return _githubApiAccessor.GetReadme(ownerLogin, repositoryName);
    }
}