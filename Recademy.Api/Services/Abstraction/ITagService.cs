using System.Collections.Generic;
using Recademy.Library.Dto;

namespace Recademy.Api.Services.Abstraction
{
    public interface ITagService
    {
        List<string> GetUserTags(int userId);
        List<string> GetAllTags();
    }
}