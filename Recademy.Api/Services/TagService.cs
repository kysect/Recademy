using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Models;
using Recademy.Library.Types;

namespace Recademy.Api.Services
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
            User userSkills = _context
                .Users
                .Include(s => s.UserSkills)
                .FirstOrDefault(s => s.Id == userId);

            if (userSkills == null)
                throw RecademyException.UserNotFound(userId);

            return userSkills
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
    }
}