using System.Collections.Generic;

namespace Recademy.Application.Services.Abstractions;

public interface ITagService
{
    IReadOnlyCollection<string> GetUserTags(int userId);
    IReadOnlyCollection<string> GetAllTags();
}