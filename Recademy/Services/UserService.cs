using System.Collections.Generic;
using Recademy.Dto;

namespace Recademy.Services
{
    public class UserService
    {
        public UserInfoDto GetUserInfo(GetUserInfoDto argues)
        {
            List<ProjectDto> projectList = new List<ProjectDto>();
            projectList.Add(new ProjectDto()
            {
                UserId = argues.UserId,
                ProjectId = 1,
                ProjectName = "lala1",
                ProjectUrl = "vk.com"
            });
            projectList.Add(new ProjectDto()
            {
                UserId = argues.UserId,
                ProjectId = 2,
                ProjectName = "lala2",
                ProjectUrl = "vk.com"
            });
            projectList.Add(new ProjectDto()
            {
                UserId = argues.UserId,
                ProjectId = 3,
                ProjectName = "lala3",
                ProjectUrl = "vk.com"
            });
            return new UserInfoDto(){Projects = projectList, Name = "Vasya"};
        }
    }
}