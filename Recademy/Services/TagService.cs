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
        private readonly RecademyContext _context;

        public TagService(RecademyContext context)
        {
            _context = context;
        }

        public List<string> GetUserTags(int userId)
        {
            return _context
                .Users
                .Include(s => s.UserSkills)
                .FirstOrDefault(s => s.Id == userId)
                .UserSkills
                .Select(s => s.SkillName)
                .ToList();
        }

        public List<string> GetAllTags()
        {
            return _context
                .Skills
                .Select(s => s.Name)
                .ToList();
        }

        public TagProfileDto GetTagProfile(string tagName)
        {
            List<ProjectInfo> projects = _context
                .ProjectInfos
                .Include(p => p.Skills)
                .Where(p => p
                        .Skills
                        .Any(s => s.SkillName == tagName))
                .ToList();

            return new TagProfileDto
            {
                TagName = tagName,
                Projects = projects
                    .Select(ProjectDto.Of)
                    .ToList()
            };
        }
    }
}