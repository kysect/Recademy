using System.Collections.Generic;

namespace Recademy.Dto
{
    public class UserInfoDto
    {
        public string UserName { get; set; }
        public List<string> Skills { get; set; }
        public List<int> Activities { get; set; }
        public List<ProjectDto> ProjectDtos { get; set; }
        public List<AchievementsDto> Achievements { get; set; }
    }
}
