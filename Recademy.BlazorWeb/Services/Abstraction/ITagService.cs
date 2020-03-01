using System.Collections.Generic;
using Recademy.BlazorWeb.Dto;

namespace Recademy.BlazorWeb.Services.Abstraction
{
    public interface ITagService
    {
        List<string> GetUserTags(int userId);
        List<string> GetAllTags();
        TagProfileDto GetTagProfile(string tagName);
    }
}