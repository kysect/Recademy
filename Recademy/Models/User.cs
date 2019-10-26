using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Models
{
    public class User
    {
        public User()
        {
            UserSkills = new List<UserSkill>();
            ProjectInfos = new List<ProjectInfo>();
            ReviewRequests = new List<ReviewRequest>();
        }

        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string GithubLink { get; set; }

        public ICollection<UserSkill> UserSkills { get; set; }

        public ICollection<ProjectInfo> ProjectInfos { get; set; }

        public ICollection<ReviewRequest> ReviewRequests { get; set; }




    }
}
