using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Recademy.Library.Types;

namespace Recademy.Library.Models
{
    public class User
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        //TODO: rename to GithubLogin
        public string GithubLink { get; set; }
        public UserType UserType { get; set; }

        public ICollection<UserSkill> UserSkills { get; set; }
        public ICollection<ProjectInfo> ProjectInfos { get; set; }
        public ICollection<ReviewRequest> ReviewRequests { get; set; }
        public ICollection<ReviewResponse> ReviewResponses { get; set; }
    }
}