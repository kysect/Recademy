using System.Collections.Generic;
using System.Linq;
using Recademy.Library.Models;
using Recademy.Library.Tools;
using Recademy.Library.Types;

namespace Recademy.Library.Dto
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
            Skills = userInfo.UserSkills.Maybe(el => el.SkillName);

            ProjectDtos = userInfo
                .ProjectInfos
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
    }
}
