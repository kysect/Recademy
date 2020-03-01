using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Recademy.Library.Dto;

namespace Recademy.Api.Services.Abstraction
{
    public interface IGithubService
    {
        List<GhRepositoryDto> GhGetRepositories(int userId);
        Task CreateIssues(string repoLink, string issueText);
        MarkupString GetReadme(string repoLink);
    }
}