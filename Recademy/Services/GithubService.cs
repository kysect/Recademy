using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Recademy.Dto;
using Recademy.Utils;
using Octokit;
using Recademy.Services.Abstraction;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace Recademy.Services
{
    public class GithubService : IGithubService
    {
        public List<GhRepositoryDto> GhGetRepositories(GhGetRepositoriesDto argues)
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
                repoList.Add(new GhRepositoryDto()
                {
                    RepositoryName = repository.Name,
                    RepositoryUrl = repository.Url
                });
            }

            return repoList;
        }
    }
}