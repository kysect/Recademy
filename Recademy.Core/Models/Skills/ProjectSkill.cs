using Recademy.Core.Models.Projects;

using System.ComponentModel.DataAnnotations.Schema;

namespace Recademy.Core.Models.Skills;

public class ProjectSkill
{
    [ForeignKey("ProjectInfo")]
    public int ProjectId { get; set; }
    public virtual ProjectInfo ProjectInfo { get; set; }

    [ForeignKey("Skill")]
    public string SkillName { get; set; }
    public virtual Skill Skill { get; set; }
}