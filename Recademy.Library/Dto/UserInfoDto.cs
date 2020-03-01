using System.Collections.Generic;
using System.Linq;
using Recademy.Library.Models;

namespace Recademy.Library.Dto
{
    public class UserInfoDto
    {
        public UserInfoDto(User userInfo)
        {
            UserName = userInfo.Name;
            Skills = userInfo
                .UserSkills
                .Select(el => el.SkillName)
                .ToList();

            ProjectDtos = userInfo
                .ProjectInfos
                .Select(k => new ProjectInfoDto(k))
                .ToList();
        }

        public string UserName { get; set; }
        public List<string> Skills { get; set; }
        public List<int> Activities { get; set; }
        public List<ProjectInfoDto> ProjectDtos { get; set; }
        public List<AchievementsDto> Achievements { get; set; }
    }
}
