using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Models
{
    public class Skill
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<ProjectInfo> ProjectInfos { get; set; }
    }
}
