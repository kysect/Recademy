using System.Collections.Generic;

namespace Recademy.Application.Services.Abstractions
{
    public interface ITagService
    {
        List<string> GetUserTags(int userId);
        List<string> GetAllTags();
    }
}