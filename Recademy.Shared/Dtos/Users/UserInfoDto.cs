using Recademy.Core.Models.Users;
using Recademy.Core.Tools;
using Recademy.Core.Types;
using Recademy.Shared.Dtos.Achievements;
using Recademy.Shared.Dtos.Github;
using Recademy.Shared.Dtos.Projects;

namespace Recademy.Shared.Dtos.Users
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
            //Skills = userInfo.UserSkills?.Maybe(el => el.SkillName);

            //ProjectDtos = userInfo
            //    .ProjectInfos?
            //    .Select(k => new ProjectInfoDto(k))
            //    .ToList();
        }

        public UserInfoDto(
            int id, 
            string name,
            string githubUsername, 
            UserType userType, 
            GithubProfileDto githubInfo
            //IReadOnlyCollection<string> skills, 
            //IReadOnlyCollection<int> activities, 
            //IReadOnlyCollection<ProjectInfoDto> projectDtos, 
            //IReadOnlyCollection<AchievementsDto> achievements, 
            //IReadOnlyCollection<UserAchievementDto> userAchievements
            )
        {
            Id = id;
            Name = name;
            GithubUsername = githubUsername;
            UserType = userType;
            GithubInfo = githubInfo;
            //Skills = skills;
            //Activities = activities;
            //ProjectDtos = projectDtos;
            //Achievements = achievements;
            //UserAchievements = userAchievements;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public string GithubUsername { get; init; }
        public UserType UserType { get; init; }
        public GithubProfileDto GithubInfo { get; init; }

        // TODO
        //public IReadOnlyCollection<string> Skills { get; init; }
        //public IReadOnlyCollection<int> Activities { get; init; }
        //public IReadOnlyCollection<ProjectInfoDto> ProjectDtos { get; init; }
        //public IReadOnlyCollection<AchievementsDto> Achievements { get; init; }
        //public IReadOnlyCollection<UserAchievementDto> UserAchievements { get; init; }
    }
}
