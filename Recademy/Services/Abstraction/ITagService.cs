using System.Collections.Generic;
using Recademy.Dto;

namespace Recademy.Services.Abstraction
{
    public interface ITagService
    {
        List<string> GetUserTags(int userId);
        List<string> GetAllTags();
        TagProfileDto GetTagProfile(string tagName);
    }
}