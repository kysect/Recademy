using System;
using System.Collections.Generic;
using System.Linq;
using Recademy.Dto;
using Recademy.Utils;
using Octokit;
using Recademy.Services.Abstraction;
using ProductHeaderValue = Octokit.ProductHeaderValue;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Recademy.Services
{
    public class GithubService : IGithubService
    {
        public List<GhRepositoryDto> GhGetRepositories(int userId)
        {
            string accessToken = GhUtil.Token;
            string clientId = GhUtil.ClientId;
            string clientSecret = GhUtil.ClientSecret;

            GitHubClient client = new GitHubClient(new ProductHeaderValue("Recademy"));
            client.Credentials = new Credentials(accessToken);
            IEnumerable<Repository> repositories = client.Repository.GetAllForCurrent().Result.Where(k => !k.Private);
            List<GhRepositoryDto> repoList = new List<GhRepositoryDto>();
            foreach (Repository repository in repositories)
            {
                string readme;
                try
                {
                    Readme request = client.Repository.Content.GetReadme(repository.Owner.Login, repository.Name).Result;
                    readme = request.Content;
                }
                catch (AggregateException)
                {
                    readme = "No readme";
                }

                repoList.Add(new GhRepositoryDto()
                {
                    RepositoryName = repository.Name,
                    RepositoryUrl = repository.Url,
                    Readme = readme,
                    Language = repository.Language
                });
            }

            return repoList;
        }

        public async Task CreateIssues(string repoLink, string issueText)
        {
            repoLink = repoLink.Replace("/repos/", "/");
            string accessToken = GhUtil.Token;
            GitHubClient client = new GitHubClient(new ProductHeaderValue("Recademy"));
            client.Credentials = new Credentials(accessToken);
            string[] splittedUrl = repoLink.Split('/');
            string issueName = GhUtil.IssueText + "Test Reviewer";
            NewIssue issue = new NewIssue(issueName);
            issue.Body = issueText;
            await client.Issue.Create(splittedUrl[3], splittedUrl[4], issue);
        }

        public static string GetReadme(string repoLink)
        {
            string accessToken = GhUtil.Token;
            string clientId = GhUtil.ClientId;
            string clientSecret = GhUtil.ClientSecret;

            GitHubClient client = new GitHubClient(new ProductHeaderValue("Recademy"));
            client.Credentials = new Credentials(accessToken);

            string[] splittedUrl = repoLink.Split('/');
            string readme;
            try
            {
                Readme request = client.Repository.Content.GetReadme(splittedUrl[3], splittedUrl[4]).Result;
                readme = request.Content;
            }
            catch (AggregateException)
            {
                readme = "No readme";
            }

            return readme;
        }
    }
}