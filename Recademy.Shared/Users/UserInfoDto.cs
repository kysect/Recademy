using Recademy.Dto.Enums;
using Recademy.Dto.Github;

namespace Recademy.Dto.Users
{
    public class UserInfoDto
    {
        public UserInfoDto()
        {
        }

        public UserInfoDto(
            int id, 
            string name,
            string githubUsername, 
            UserTypeDto userType, 
            GithubProfileDto githubInfo)
        {
            Id = id;
            Name = name;
            GithubUsername = githubUsername;
            UserType = userType;
            GithubInfo = githubInfo;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public string GithubUsername { get; init; }
        public UserTypeDto UserType { get; init; }
        public GithubProfileDto GithubInfo { get; init; }
    }
}
