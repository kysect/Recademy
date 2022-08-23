using System;
using System.Collections.Generic;
using Markdig;
using Microsoft.AspNetCore.Components;
using Octokit;
using Recademy.Core.Dto;

namespace Recademy.Api.Tools
{
    //TODO: implement work with token
    public interface IGithubApiAccessor
    {
        Issue CreateIssue(string owner, string repositoryName, NewIssue issueInfo);
        IReadOnlyList<Repository> ReadAllUserRepositories(string token);
        MarkupString GetReadme(string login, string repositoryName);
        GithubProfileDto GetUserProfile(string login);
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

        public MarkupString GetReadme(string login, string repositoryName)
        {
            //TODO: replace try/catch with null-check
            try
            {
                string readme = _client
                    .Repository
                    .Content
                    .GetReadme(login, repositoryName)
                    .Result.Content;

                return (MarkupString)Markdown.ToHtml(readme);
            }
            catch (AggregateException)
            {
                //TODO: Replace with null, ensure that it will work fine
                return (MarkupString)"No readme";
            }
        }

        public GithubProfileDto GetUserProfile(string login)
        {
            User result = _client.User.Get(login).Result;

            return new GithubProfileDto
            {
                GithubUserId = result.Id,
                AvatarUrl = result.AvatarUrl,
                Name = result.Name,
                Bio = result.Bio,
                Company = result.Company,
                Login = result.Login
            };
        }
    }
}