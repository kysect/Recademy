using Recademy.Core.Models;
using Recademy.Core.Tools;
using Recademy.Core.Types;

namespace Recademy.Shared.Dtos
{
    public class UserInfoDto
    {
        public UserInfoDto()
        {
            
        }

        public UserInfoDto(User userInfo)
        {
            Id = userInfo.Id;
            Name = userInfo.Name;
            GithubUsername = userInfo.GithubUsername;
            UserType = userInfo.UserType;
            Skills = userInfo.UserSkills?.Maybe(el => el.SkillName);

            ProjectDtos = userInfo
                .ProjectInfos?
                .Select(k => new ProjectInfoDto(k))
                .ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string GithubUsername { get; set; }
        public UserType UserType { get; set; }
        public GithubProfileDto GithubInfo { get; set; }

        public List<string> Skills { get; set; }
        public List<int> Activities { get; set; }
        public List<ProjectInfoDto> ProjectDtos { get; set; }
        public List<AchievementsDto> Achievements { get; set; }
        public IReadOnlyCollection<UserAchievementDto> UserAchievements { get; set; }
    }
}
