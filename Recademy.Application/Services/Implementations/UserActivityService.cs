using Kysect.GithubUtils.OrganizationContributions;
using Octokit;
using Recademy.Application.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recademy.Application.Mappings;
using Recademy.Dto;
using Recademy.Dto.Activity;
using System;
using System.Linq;

namespace Recademy.Application.Services.Implementations;

public sealed class UserActivityService : IUserActivityService
{
    public async Task<IReadOnlyCollection<UserActivityDto>> GetAllActivity()
    {
        if (string.IsNullOrEmpty(GhUtil.Token))
            return Array.Empty<UserActivityDto>();

        GitHubClient client = CreateClient();

        var organizationFetcher = new OrganizationContributionFetcher(client);

        List<OrganizationContributor> result = await organizationFetcher.FetchOrganizationStatistic("kysect");
        return result
            .Select(role => role.ToDto())
            .ToList();
    }

    public async Task<UserActivityDto> GetUserActivity(string userName)
    {
        if (string.IsNullOrEmpty(GhUtil.Token))
            return null;

        IReadOnlyCollection<UserActivityDto> result = await GetAllActivity();
        return result.FirstOrDefault(a => a.Name == userName);
    }

    private GitHubClient CreateClient()
    {
        var client = new GitHubClient(new ProductHeaderValue("Recademy"))
        {
            Credentials = new Credentials(GhUtil.Token)
        };

        return client;
    }
}

