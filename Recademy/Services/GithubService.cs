using System;
using System.Collections.Generic;
using Recademy.Dto;
using Recademy.Utils;
using Octokit;

namespace Recademy.Services
{
    public class GithubService
    {
        public List<GhRepositoryDto> GhGetRepositories(GhGetRepositoriesDto argues)
        {
            string accessToken = GhUtil.Token;
            string clientId = GhUtil.ClientId;
            string clientSecret = GhUtil.ClientSecret;

            GitHubClient client = new GitHubClient(new ProductHeaderValue("Recademy"));
            client.Credentials = new Credentials(accessToken);

            var repositories = client.Repository.GetAllPublic().Result;

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