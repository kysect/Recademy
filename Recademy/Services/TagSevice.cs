using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Context;
using Recademy.Dto;
using Recademy.Services.Abstraction;

namespace Recademy.Services
{
    public class TagSevice : ITagSevice
    {
        public RecademyContext Context;

        public TagSevice(RecademyContext context)
        {
            Context = context;
        }

        public TagsDto GetUserTags(int userId)
        {
            TagsDto tags = new TagsDto();
            tags.Tags = Context.Users.Include(s => s.UserSkills).FirstOrDefault(s => s.Id == userId).UserSkills.Select(s => s.SkillName).ToList();
            return tags;
        }

        public TagsDto GetAllTags()
        {
            TagsDto tags = new TagsDto();
            tags.Tags = Context.Skills.Select(s => s.Name).ToList();
            return tags;
        }
    }
}