using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Recademy.Core.Types;

namespace Recademy.Core.Models
{
    public class User
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        public string GithubUsername { get; set; }
        public UserType UserType { get; set; }
        public ICollection<UserSkill> UserSkills { get; set; }
        public ICollection<ProjectInfo> ProjectInfos { get; set; }
        public ICollection<ReviewRequest> ReviewRequests { get; set; }
        public ICollection<ReviewResponse> ReviewResponses { get; set; }
    }
}