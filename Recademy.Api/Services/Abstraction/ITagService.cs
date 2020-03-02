using System.Collections.Generic;

namespace Recademy.Api.Services.Abstraction
{
    public interface ITagService
    {
        List<string> GetUserTags(int userId);
        List<string> GetAllTags();
    }
}