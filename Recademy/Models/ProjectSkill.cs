using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recademy.Models
{
    public class ProjectSkill
    {
        [ForeignKey("ProjectInfo")]
        public int ProjectId { get; set; }
        
        [ForeignKey("Skill")]
        public string SkillName { get; set; }
    }
}
