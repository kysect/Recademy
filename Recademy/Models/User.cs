using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string GithubLink { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public ICollection<ProjectInfo> ProjectInfos { get; set; }

        public ICollection<ReviewRequest> ReviewRequests { get; set; }




    }
}
