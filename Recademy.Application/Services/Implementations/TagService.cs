using Microsoft.EntityFrameworkCore;

using Recademy.Application.Services.Abstractions;
using Recademy.Core.Models.Users;
using Recademy.Core.Types;
using Recademy.DataAccess;

using System.Collections.Generic;
using System.Linq;

namespace Recademy.Application.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly RecademyContext _context;

        public TagService(RecademyContext context)
        {
            _context = context;
        }

        public IReadOnlyCollection<string> GetUserTags(int userId)
        {
            RecademyUser userSkills = _context
                .RecademyUsers
                .Include(s => s.UserSkills)
                .FirstOrDefault(s => s.UserId == userId);

            if (userSkills == null)
                throw RecademyException.UserNotFound(userId);

            return userSkills
                .UserSkills
                .Select(s => s.SkillName)
                .ToList();
        }

        public IReadOnlyCollection<string> GetAllTags()
        {
            return _context
                .Skills
                .Select(s => s.Name)
                .ToList();
        }
    }
}