﻿using System.Collections.Generic;

namespace Recademy.Library.Dto
{
    public class UserInfoDto
    {
        public string UserName { get; set; }
        public List<string> Skills { get; set; }
        public List<int> Activities { get; set; }
        public List<ProjectInfoDto> ProjectDtos { get; set; }
        public List<AchievementsDto> Achievements { get; set; }
    }
}
