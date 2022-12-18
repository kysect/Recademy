using Kysect.GithubUtils.OrganizationContributions;
using Recademy.Dto.Activity;

namespace Recademy.Application.Mappings;

public static class ActivityMappingExtensions
{
    public static UserActivityDto ToDto(this OrganizationContributor organizationContributor)
    {
        if (organizationContributor is null)
            return null;

        return new UserActivityDto()
        {
            Name = organizationContributor.Username,
            ContributionsCount = organizationContributor.Contributions
        };
    }
}