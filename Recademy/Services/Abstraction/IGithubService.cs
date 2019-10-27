using System.Collections.Generic;
using System.Threading.Tasks;
using Recademy.Dto;

namespace Recademy.Services.Abstraction
{
    public interface IGithubService
    {
        List<GhRepositoryDto> GhGetRepositories(int userId);
        Task CreateIssues(string repoLink, string issueText);
    }
}