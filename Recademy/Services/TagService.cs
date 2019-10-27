using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Context;
using Recademy.Dto;
using Recademy.Models;
using Recademy.Services.Abstraction;

namespace Recademy.Services
{
    public class TagService : ITagService
    {
        public RecademyContext Context;

        public TagService(RecademyContext context)
        {
            Context = context;
        }

        public List<string> GetUserTags(int userId)
        {
            
            List<string> tags = Context.Users.Include(s => s.UserSkills)
                .FirstOrDefault(s => s.Id == userId).UserSkills
                .Select(s => s.SkillName).ToList();
            return tags;
        }

        public List<string> GetAllTags()
        {
            List<string> tags = Context.Skills.Select(s => s.Name).ToList();
            return tags;
        }

        public TagProfileDto GetTagProfile(string tagName)
        {
            List<ProjectInfo> projects = Context.ProjectInfos
                .Include(p => p.Skills)
                .Where(p => p.Skills.Any(s => s.SkillName == tagName))
                .ToList();

            return new TagProfileDto
            {
                TagName = tagName,
                Projects = projects.Select(ProjectDto.Of).ToList()
            };
        }
    }
}