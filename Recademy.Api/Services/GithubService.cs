using System;
using System.Collections.Generic;
using System.Linq;
using Markdig;
using Microsoft.AspNetCore.Components;
using Octokit;
using Recademy.Api.Services.Abstraction;
using Recademy.Api.Tools;
using Recademy.Library.Dto;

namespace Recademy.Api.Services
{
    public class GithubService : IGithubService
    {
        private readonly IGithubApiAccessor _githubApiAccessor;

        public GithubService(IGithubApiAccessor githubApiAccessor)
        {
            _githubApiAccessor = githubApiAccessor;
        }

        public List<GhRepositoryDto> GhGetRepositories(int userId)
        {
            IReadOnlyList<Repository> repositories = _githubApiAccessor.ReadAllUserRepositories(String.Empty);

            return repositories
                .Where(k => !k.Private)
                .Select(k => new GhRepositoryDto
                {
                    RepositoryName = k.Name,
                    RepositoryUrl = k.Url,
                    Readme = GetReadme(k),
                    Language = k.Language
                })
                .ToList();
        }

        public Issue CreateIssues(string projectUrl, string issueText)
        {
            projectUrl = projectUrl.Replace("/repos/", "/", StringComparison.InvariantCultureIgnoreCase);
            string[] splittedUrl = projectUrl.Split('/');
            string issueName = GhUtil.IssueText + "Test Reviewer";
            NewIssue issue = new NewIssue(issueName)
            {
                Body = issueText
            };

            return _githubApiAccessor.CreateIssue(splittedUrl[3], splittedUrl[4], issue);
        }

        public MarkupString GetReadme(string projectUrl)
        {
            string[] splittedUrl = projectUrl.Split('/');
            return GetReadme(splittedUrl[3], splittedUrl[4]);
        }

        private MarkupString GetReadme(Repository repository)
        {
            return GetReadme(repository.Owner.Login, repository.Name);
        }

        private MarkupString GetReadme(string login, string repositoryName)
        {
            //TODO: replace try/catch with null-check
            try
            {
                return (MarkupString) Markdown.ToHtml(
                    _githubApiAccessor
                        .ReadRepositoryReadme(login, repositoryName)
                        .Content);
            }
            catch (AggregateException)
            {
                //TODO: Replace with null, ensure that it will work fine
                return (MarkupString)"No readme";
            }
        }
    }
}