using System;
using System.Collections.Generic;
using System.Linq;
using Recademy.Dto;
using Recademy.Utils;
using Octokit;
using Recademy.Services.Abstraction;
using ProductHeaderValue = Octokit.ProductHeaderValue;
using System;

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
            var repositories = client.Repository.GetAllForCurrent().Result.Where(k => !k.Private);

            List<GhRepositoryDto> repoList = new List<GhRepositoryDto>();
            foreach (var repository in repositories)
            {
                string readme;
                try
                {
                    var request = client.Repository.Content.GetReadme(repository.Owner.Login, repository.Name).Result;
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
    }
}