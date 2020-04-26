﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Library.Models;
using Recademy.Library.Types;

namespace Recademy.Api.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly RecademyContext _context;

        public UserRepository(RecademyContext context)
        {
            _context = context;
        }

        public User Find(int id)
        {
            return _context.Users
                .Include(s => s.ProjectInfos)
                .ThenInclude(p => p.Skills)
                .Include(s => s.UserSkills)
                .Include(u => u.ReviewRequests)
                .FirstOrDefault(s => s.Id == id);
        }

        public User Get(int id)
        {
            return Find(id) ?? throw RecademyException.UserNotFound(id);
        }

        public User FindByUsername(string username)
        {
            return _context.Users
                .Include(s => s.ProjectInfos)
                .ThenInclude(p => p.Skills)
                .Include(s => s.UserSkills)
                .Include(u => u.ReviewRequests)
                .SingleOrDefault(s => s.GithubLink == username);
        }

        public User UpdateUserRole(User user, UserType userType)
        {
            user.UserType = userType;
            _context.SaveChanges();

            return user;
        }
    }
}