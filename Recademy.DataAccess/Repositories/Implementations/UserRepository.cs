using Microsoft.EntityFrameworkCore;

using Recademy.Core.Models.Users;
using Recademy.Core.Types;
using Recademy.DataAccess.Repositories.Abstractions;

using System.Linq;

namespace Recademy.DataAccess.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly RecademyContext _context;

        public UserRepository(RecademyContext context)
        {
            _context = context;
        }

        public RecademyUser Find(int id)
        {
            return _context.RecademyUsers
                .Include(s => s.ProjectInfos)
                .ThenInclude(p => p.Skills)
                .Include(s => s.UserSkills)
                .Include(u => u.ReviewRequests)
                .FirstOrDefault(s => s.UserId == id);
        }

        public RecademyUser Get(int id)
        {
            return Find(id) ?? throw RecademyException.UserNotFound(id);
        }

        public RecademyUser FindRecademyUser(string username)
        {
            return _context.RecademyUsers
                .Include(s => s.ProjectInfos)
                .ThenInclude(p => p.Skills)
                .Include(s => s.UserSkills)
                .Include(u => u.ReviewRequests)
                .SingleOrDefault(s => s.User.GithubUsername == username);
        }

        public User? FindUser(string username)
        {
            return _context.Users.SingleOrDefault(user => user.GithubUsername == username);
        }

        public User UpdateUserRole(User user, UserType userType)
        {
            user.UserType = userType;
            _context.SaveChanges();

            return user;
        }
    }
}