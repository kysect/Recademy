using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Recademy.Api.Services.Abstraction;
using Recademy.Library.Dto;
using Recademy.Library.Models;
using Recademy.Library.Types;

namespace Recademy.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IAchievementService _achievements;
        private readonly RecademyContext _context;

        public UserService(RecademyContext context, IAchievementService achievements)
        {
            _context = context;
            _achievements = achievements;
        }

        public UserInfoDto GetUserInfo(int userId)
        {
            User userInfo = _context.Users
                .Include(s => s.ProjectInfos)
                .ThenInclude(p => p.Skills)
                .Include(s => s.UserSkills)
                .Include(u => u.ReviewRequests)
                .FirstOrDefault(s => s.Id == userId);

            if (userInfo == null)
                throw RecademyException.UserNotFound(userId);

            return new UserInfoDto(userInfo)
            {
                Activities = GetUserActivityPerMonth(userId),
                Achievements = _achievements.GetAchievements(userInfo),
            };
        }

        public List<ProjectInfoDto> ReadUserProjects(int userId)
        {
            if (!_context.Users.Any(u => u.Id == userId))
                throw RecademyException.UserNotFound(userId);

            return _context.ProjectInfos
                .Include(p => p.Skills)
                .Include(p => p.ReviewRequests)
                .Where(p => p.AuthorId == userId)
                .Select(p => new ProjectInfoDto(p))
                .ToList();
        }

        /// <summary>
        ///     return a user activity, index is month, value is activity number
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<int> GetUserActivityPerMonth(int userId)
        {
            List<ReviewResponse> reviewList = _context
                .ReviewResponses
                .Where(x => x.ReviewerId == userId)
                .ToList();

            List<int> result = Enumerable.Repeat(0, 12).ToList();

            foreach (ReviewResponse el in reviewList)
                result[el.CreationTime.Month]++;

            return result;
        }

        /// <summary>
        ///     get a score ranking by user's activities
        ///     key is user id, value is activity score
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetUsersRanking()
        {
            int ActivityInCount(int userId)
            {
                return _context
                    .ReviewResponses
                    .Count(r => r.ReviewerId == userId);
            }

            return _context
                .Users
                .ToList()
                .Select(u => (u.Name, Points: ActivityInCount(u.Id)))
                .OrderByDescending(t => t.Points)
                .ToDictionary(t => t.Name, t => t.Points);
        }
    }
}