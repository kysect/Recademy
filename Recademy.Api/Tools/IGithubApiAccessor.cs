using System;
using System.Collections.Generic;
using Octokit;

namespace Recademy.Api.Tools
{
    //TODO: implement work with token
    public interface IGithubApiAccessor
    {
        Issue CreateIssue(string owner, string repositoryName, NewIssue issueInfo);

        IReadOnlyList<Repository> ReadAllUserRepositories(string token);
        Readme ReadRepositoryReadme(string owner, string repositoryName);
    }

    public class GithubApiAccessor : IGithubApiAccessor
    {
        private readonly GitHubClient _client = new GitHubClient(new ProductHeaderValue("Recademy"))
        {
            Credentials = new Credentials(GhUtil.Token)
        };

        public Issue CreateIssue(String owner, String repositoryName, NewIssue issueInfo)
        {
            return _client
                .Issue
                .Create(owner, repositoryName, issueInfo)
                .Result;
        }

        public IReadOnlyList<Repository> ReadAllUserRepositories(String token)
        {
            return _client
                .Repository
                .GetAllForCurrent()
                .Result;
        }

        public Readme ReadRepositoryReadme(String owner, String repositoryName)
        {
            return _client
                .Repository
                .Content
                .GetReadme(owner, repositoryName)
                .Result;
        }
    }
}