using Recademy.Dto;

namespace Recademy.Services.Abstraction
{
    public interface ITagSevice
    {
        TagsDto GetUserTags(int userId);
        TagsDto GetAllTags();
    }
}