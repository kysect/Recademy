using Kysect.GithubUtils.OrganizationContributions;
using Octokit;
using Recademy.Application.Services.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recademy.Application.Mappings;
using Recademy.Dto.Activity;
using System.Linq;

namespace Recademy.Application.Services.Implementations;

public sealed class UserActivityService : IUserActivityService
{
    private readonly GitHubClient _client = new(new ProductHeaderValue("Recademy"));
    public async Task<IReadOnlyCollection<UserActivityDto>> GetAllActivity()
    {
        var organizationFetcher = new OrganizationContributionFetcher(_client);

        List<OrganizationContributor> result = await organizationFetcher.FetchOrganizationStatistic("kysect");
        return result
            .Select(role => role.ToDto())
            .ToList();
    }

    public async Task<UserActivityDto> GetUserActivity(string userName)
    {
        IReadOnlyCollection<UserActivityDto> result = await GetAllActivity();
        return result.FirstOrDefault(a => a.Name == userName);
    }
}

