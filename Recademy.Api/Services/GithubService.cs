using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markdig;
using Microsoft.AspNetCore.Components;
using Octokit;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;

namespace Recademy.Api.Services
{
    public class GithubService : IGithubService
    {
        private readonly GitHubClient _client = new GitHubClient(new ProductHeaderValue("Recademy"))
        {
            Credentials = new Credentials(GhUtil.Token)
        };

        public List<GhRepositoryDto> GhGetRepositories(int userId)
        {
            return _client
                .Repository
                .GetAllForCurrent()
                .Result
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

        public async Task CreateIssues(string repoLink, string issueText)
        {
            repoLink = repoLink.Replace("/repos/", "/");
            var splittedUrl = repoLink.Split('/');
            string issueName = GhUtil.IssueText + "Test Reviewer";
            NewIssue issue = new NewIssue(issueName)
            {
                Body = issueText
            };

            await _client
                .Issue
                .Create(splittedUrl[3], splittedUrl[4], issue);
        }

        public MarkupString GetReadme(string repoLink)
        {
            var splittedUrl = repoLink.Split('/');
            return GetReadme(splittedUrl[3], splittedUrl[4]);
        }

        public MarkupString GetReadme(Repository repository)
        {
            return GetReadme(repository.Owner.Login, repository.Name);
        }

        private MarkupString GetReadme(string login, string repositoryName)
        {
            //TODO: replace try/catch with null-check
            try
            {
                return (MarkupString)Markdown.ToHtml(_client
                    .Repository
                    .Content
                    .GetReadme(login, repositoryName)
                    .Result
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