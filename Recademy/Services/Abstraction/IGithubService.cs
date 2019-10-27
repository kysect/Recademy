using System.Collections.Generic;
using Recademy.Dto;

namespace Recademy.Services.Abstraction
{
    public interface IGithubService
    {
        List<GhRepositoryDto> GhGetRepositories(int userId);
    }
}