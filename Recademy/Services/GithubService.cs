using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Recademy.Dto;
using Recademy.Services.Abstraction;
using Recademy.Utils;
using Markdig;

namespace Recademy.Services
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

        public string GetReadme(string repoLink)
        {
            var splittedUrl = repoLink.Split('/');
            return GetReadme(splittedUrl[3], splittedUrl[4]);
        }

        public string GetReadme(Repository repository)
        {
            return GetReadme(repository.Owner.Login, repository.Name);
        }

        private string GetReadme(string login, string repositoryName)
        {
            //TODO: replace try/catch with null-check
            try
            {
                return Markdown.ToHtml(_client
                    .Repository
                    .Content
                    .GetReadme(login, repositoryName)
                    .Result
                    .Content);
            }
            catch (AggregateException)
            {
                //TODO: Replace with null, ensure that it will work fine
                return "No readme";
            }
        }
    }
}